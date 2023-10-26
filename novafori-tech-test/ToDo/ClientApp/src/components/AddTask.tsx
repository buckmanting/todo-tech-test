import React, {FormEvent, useContext, useState} from 'react';
import {UserTask} from "../models/UserTask";
import {TaskContext} from "../state/tasks.context";
import {CurrentUserContext} from "./Home";
import {Link, useNavigate} from "react-router-dom";

export default (props: AddTaskProps) => {
    const taskContext = useContext(TaskContext);
    const currentUserContext = useContext(CurrentUserContext);

    const navigate = useNavigate();

    const [description, setDescription] = useState('');

    return (
        <form
            onSubmit={(event: FormEvent) => {
                event.preventDefault();

                // @ts-ignore
                const task = {
                    createdAt: Date.now(),
                    description,
                    isDone: false
                } as UserTask;

                fetch(`/tasks/${currentUserContext.id}/create`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(task)
                })
                    .then(response => response.json())
                    .then(data => {
                        taskContext.setCurrentTasks([...taskContext.tasks, data]);
                        navigate('../');
                    });
            }}
        >
            <p>
                <Link to={'/'}>Cancel</Link>
            </p>
            <h2>Add a new Task</h2>
            <p>
                <label htmlFor="task-description">New Task:</label>
                <input
                    id="task-description" type="text"
                    value={description} onChange={e => setDescription(e.target.value)}
                    required/>
            </p>
            <button type="submit">Submit</button>
        </form>
    )
}

export interface AddTaskProps {
    onChange: (event: FormEvent) => void;
}