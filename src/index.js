import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { Provider } from 'react-redux';

import store from './store/store'
import Home from './pages/Home';
import ArticlePage from './pages/ArticlePage'
import NewsPage from './pages/NewsPage';
import BlindspotsPage from './pages/BlindspotsPage';
import ErrorPage from './pages/ErrorPage';
import AdminPage from './pages/admin pages/AdminPage';
import UploadPublisherPage from './pages/admin pages/UploadPublisherPage';
import UploadArticlePage from './pages/admin pages/UploadArticlePage';
import UploadCategoryPage from './pages/admin pages/UploadCategoryPage';
import RegistrationPage from './pages/auth pages/RegistrationPage';
import LoginPage from './pages/auth pages/LoginPage';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Home />,
    errorElement: <ErrorPage />
  },
  {
    path: '/article/:id',
    element: <ArticlePage />
  },
  {
    path: '/news',
    element: <NewsPage />
  },
  {
    path: '/blindspots',
    element: <BlindspotsPage />
  },
  {
    path: '/admin',
    element: <AdminPage />
  },
  {
    path: '/admin/publisher',
    element: <UploadPublisherPage />
  },
  {
    path: '/admin/publisher/:id',
    element: <UploadPublisherPage />
  },
  {
    path: '/admin/category',
    element: <UploadCategoryPage />
  },
  {
    path: '/admin/category/:id',
    element: <UploadCategoryPage />
  },
  {
    path: '/admin/article',
    element: <UploadArticlePage />
  },
  // {
  //   path: '/register',
  //   element: <RegistrationPage />
  // },
  {
    path: '/login',
    element: <LoginPage />
  }
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
          <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);