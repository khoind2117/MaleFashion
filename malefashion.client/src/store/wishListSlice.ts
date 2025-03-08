import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: number[] = (JSON.parse(localStorage.getItem("wishlist") || "[]") as unknown[]).map(Number);

const wishListSlice = createSlice({
    name: "wishlist",
    initialState,
    reducers: {
        toggleWishList: (state, action: PayloadAction<number>) => {
            const index = state.indexOf(action.payload);
            if (index === -1) {
                state.push(action.payload);
            } else {
                state.splice(index, 1);
            }
            localStorage.setItem("wishlist", JSON.stringify(state));
        },
    },
});

export const { toggleWishList } = wishListSlice.actions;
export default wishListSlice.reducer;
