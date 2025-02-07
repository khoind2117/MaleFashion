import { configureStore } from '@reduxjs/toolkit';
import cartReducer from './cartSlice';
import notificationMiddleware from '../middlewares/notificationMiddleware';

const store = configureStore({
  reducer: {
    cart: cartReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(notificationMiddleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;