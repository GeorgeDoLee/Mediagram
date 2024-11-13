import { configureStore } from '@reduxjs/toolkit';
import userReducer from './reducers/userReducers';

const userInfoFromStorage = localStorage.getItem('admin') ? JSON.parse(localStorage.getItem('admin')) : null;

const store = configureStore({
    reducer: {
        user: userReducer
    },
    preloadedState: {
        user: { userInfo: userInfoFromStorage }
    }
})

export default store;