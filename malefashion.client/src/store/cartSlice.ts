import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { CartItemDto } from "../models/dtos/CartItem/CartItemDto";
import { CartDto } from "../models/dtos/Cart/CartDto";
import cartApi from "../services/api/cartApi";
import { AppThunk } from "./store";
import { toast } from "react-toastify";
import { CartItemRequestDto } from "../models/dtos/CartItem/CartItemRequestDto";

interface CartState {
  userId: string | null;
  basketId: string | null;
  lastUpdated: Date | null;
  items: CartItemDto[];
  isMerged: boolean;
}

const initialState: CartState = {
  userId: null,
  basketId: null,
  lastUpdated: null,
  items: [],
  isMerged: false,
};

const cartSlice = createSlice({
  name: "cart",
  initialState: initialState,
  reducers: {
    setCartFromAPI(state, action: PayloadAction<CartDto>) {
      const cart = action.payload;
      state.userId = cart.userId;
      state.basketId = cart.basketId;
      state.lastUpdated = cart.lastUpdated;
      state.items = cart.cartItemDtos;
    },
    resetCart: () => initialState,
    setCartMerged: (state) => {
      state.isMerged = true;
    }
  },
});

export const { setCartFromAPI, resetCart, setCartMerged } = cartSlice.actions;

export const addToCart =
(cartItemRequestDto: CartItemRequestDto): AppThunk =>
async (dispatch) => {
  try {
    const response = await cartApi.addToCart(cartItemRequestDto);
    if (response.status === 200) {
      toast.success("Item added to cart successfully");
      const getCartResponse = await cartApi.getCart();
      dispatch(setCartFromAPI(getCartResponse.data));
    } else {
      toast.error("Failed to add item to cart. Please try again.");
    }
  } catch (error) {
    console.error("Error adding item to cart:", error);
    toast.error("An error occurred while adding item to cart. Please try again.");
  }
};

export const removeFromCart =
(productVariantId: number): AppThunk =>
async (dispatch) => {
  try {
    const response = await cartApi.removeFromCart(productVariantId);
    
    if (response.status === 204) {
      toast.warning("Item removed from cart successfully");
      const getCartResponse = await cartApi.getCart();
      dispatch(setCartFromAPI(getCartResponse.data));
    }
    else {
      toast.error("Failed to remove item from cart. Please try again.");
    }
  } catch (error) {
    console.error("Error removing item from cart:", error);
    toast.error("An error occurred while removing item from cart. Please try again.");
  }
};

export const mergeCart = 
(): AppThunk =>
async (dispatch) => {
  try {
    const response = await cartApi.mergeCart();
    
    if (response.status === 200) {
      toast.success("Items previously added are also merged into the cart. Please check your cart.")
      const getCartResponse = await cartApi.getCart();
      dispatch(setCartFromAPI(getCartResponse.data));
      dispatch(setCartMerged());
    }
    else {
      toast.error("Failed to merge cart. Please try again.");
      toast.error("An error occurred while merging item from cart. Please try again.");
    }
  }
  catch (error) {
    console.error("Error merging cart:", error);
    toast.error("An error occurred while merging the cart. Please try again.");
  }
}

export default cartSlice.reducer;
