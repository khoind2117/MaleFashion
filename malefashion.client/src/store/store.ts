import { Action, configureStore } from '@reduxjs/toolkit';
import cartReducer from './cartSlice';
import wishListReducer from './wishListSlice';
import recentlyViewReducer from './recentlyViewSlice';
import notificationMiddleware from '../middlewares/notificationMiddleware';
import { ThunkAction } from 'redux-thunk';

const store = configureStore({
  reducer: {
    cart: cartReducer,
    wishList: wishListReducer,
    recentlyView: recentlyViewReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(notificationMiddleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;

export default store;