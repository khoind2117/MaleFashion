import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: number[] = JSON.parse(localStorage.getItem("recentlyViewed") || "[]");

const recentlyViewSlice = createSlice({
    name: "recentlyView",
    initialState,
    reducers: {
        addRecentlyViewed: (state, action: PayloadAction<number>) => {
            if (!state.includes(action.payload)) {
                state.unshift(action.payload);
                if (state.length > 10) state.pop();
            }
            localStorage.setItem("recentlyViewed", JSON.stringify(state));
        },
    },
});

export const { addRecentlyViewed } = recentlyViewSlice.actions;
export default recentlyViewSlice.reducer;
