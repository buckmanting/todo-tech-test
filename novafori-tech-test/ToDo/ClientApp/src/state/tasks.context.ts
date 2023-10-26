import {UserTask} from "../models/UserTask";
import {createContext} from "react";

export interface TaskContext {
    tasks: UserTask[];
    setCurrentTasks: (tasks: UserTask[]) => void;
}

const DEFAULT_TASKS_CONTEXT = {
    tasks: [] as UserTask[],
    setCurrentTasks: (tasks: UserTask[]) => {
    }
}
export const TaskContext = createContext(DEFAULT_TASKS_CONTEXT);