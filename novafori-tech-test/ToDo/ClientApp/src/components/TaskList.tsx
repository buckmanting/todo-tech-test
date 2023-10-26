import React, {useContext, useMemo, useRef} from 'react';
import {TaskContext} from "../state/tasks.context";
import {UserTask} from "../models/UserTask";
import {CurrentUserContext} from "./Home";
import './TaskList.css';

export default () => {
    const taskContext = useContext(TaskContext);
    const currentUserContext = useContext(CurrentUserContext);

    const printTask = (task: UserTask) => {
        const createdDate = new Date(task.createdAt);
        return (
            <li key={task.id} className={'slide-in-left c-task-list__task'}>
                {task.description}
                <span className={'c-task-list__item-date'}>{`${createdDate.getDate()}-${createdDate.getMonth()}-${createdDate.getFullYear()}`}</span>
                <button onClick={(event) => {
                    // @ts-ignore
                    event.currentTarget.parentElement.classList.toggle('slide-out-right');

                    fetch(`/tasks/${currentUserContext.id}/${task.id}/update`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({...task, isDone: !task.isDone})
                    })
                        .then(response => response.json())
                        .then(data => {
                            const taskListClone = [...taskContext.tasks];
                            const index = taskListClone.findIndex(t => t.id === task.id);
                            taskListClone[index] = data;
                            taskContext.setCurrentTasks(taskListClone);
                        });
                }}>{task.isDone ? 'Undo' : 'Done'}</button>
            </li>);
    }

    return (
        <>
            <div className={'c-task-list__container'}>
                <section className={'c-task-list__item'}>
                    <h2>Pending Tasks</h2>
                    <ul>
                        {useMemo(() => {
                            return taskContext.tasks.map((task) => {
                                if (!task.isDone) {
                                    return printTask(task)
                                }
                            })
                        }, [taskContext.tasks])
                        }
                    </ul>
                </section>
                <section className={'c-task-list__item--completed'}>
                    <h2>Done Tasks</h2>
                    <ul>
                        {useMemo(() => {
                            return taskContext.tasks.map((task) => {
                                if (task.isDone) {
                                    return printTask(task)
                                }
                            })
                        }, [taskContext.tasks])
                        }
                    </ul>
                </section>
            </div>
        </>
    )
        ;
}

export interface TaskListProps {
    onComplete: (id: string) => void;
}