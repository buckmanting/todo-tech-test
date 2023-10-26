import React, {createContext, useContext, useEffect, useMemo, useState} from 'react';
import {Link} from "react-router-dom";
import TaskList from "./TaskList";
import {User} from "../models/User";
import {TaskContext} from "../state/tasks.context";
import "./Home.css"

export const CurrentUserContext = createContext({} as User);
export default (props: HomeProps) => {
    const taskContext = useContext(TaskContext);
    const currentUserContext = useContext(CurrentUserContext);

    useEffect(() => {
        if (currentUserContext && currentUserContext.id) {
            fetch(`/tasks/${currentUserContext.id}`)
                .then(response => response.json())
                .then(data => taskContext.setCurrentTasks(data));
        }
    }, [currentUserContext]);

    return (
        <>
            <div className={'c-home__heading-container'}>
                <h1 className={'c-home__heading-container-item'}>Your Todo List</h1>
                <Link className={'c-home__heading-container-item c-home__button'} to='/add-task'>Add a Task</Link>
            </div>
            <TaskList/>
        </>
    );
}

export interface HomeProps {
}
