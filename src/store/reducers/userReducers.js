import { createSlice } from "@reduxjs/toolkit";

const userSlice = createSlice({
    name: 'user',
    initialState: {
        userInfo: null,
    },
    reducers: {
        setUserInfo(state, action){
            state.userInfo = action.payload
        },
        resetUserInfo(state){
            state.userInfo = null
        }
    }
});

export const {setUserInfo, resetUserInfo} = userSlice.actions;
export default userSlice.reducer;