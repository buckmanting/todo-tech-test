import AddTask from "./components/AddTask";
import Home from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/add-task',
    element: <AddTask />
  }
];

export default AppRoutes;
