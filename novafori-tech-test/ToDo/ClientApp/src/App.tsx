import React, {Component, useMemo, useState} from 'react';
import {Route, Routes} from 'react-router-dom';
import AppRoutes from './AppRoutes';
import {Layout} from './components/Layout';
import './custom.css';
import {TaskContext} from "./state/tasks.context";
import useTasks from "./hooks/useTasks";
import {User} from "./models/User";
import {CurrentUserContext} from "./components/Home";

export default (props: any) => {
    const tasks = useTasks();
    const [currentUser, setCurrentUser] = useState({} as User);

    useMemo(() => {
        fetch('currentUser')
            .then(response => response.json())
            .then(data => setCurrentUser(data));
    }, []);

    return (
        <CurrentUserContext.Provider value={currentUser}>
            <TaskContext.Provider value={tasks}>
                <Layout>
                    <Routes>
                        {AppRoutes.map((route, index) => {
                            const {element, ...rest} = route;
                            return <Route key={index} {...rest} element={element}/>;
                        })}
                    </Routes>
                </Layout>
            </TaskContext.Provider>
        </CurrentUserContext.Provider>
    );
}
