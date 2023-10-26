import {TaskContext} from "../state/tasks.context";
import {useCallback, useState} from "react";
import {UserTask} from "../models/UserTask";

export default (): TaskContext => {
    const [tasks, setTasks] = useState([] as UserTask[]);
    const setCurrentTasks = useCallback((tasks: UserTask[]) => {
        console.log('setTasks', tasks);
        setTasks(tasks);
    }, [])
    
    return {
        tasks,
        setCurrentTasks
    };
}