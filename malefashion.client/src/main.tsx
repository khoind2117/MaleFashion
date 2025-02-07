import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { Provider } from 'react-redux'
import App from './App.tsx'
import './assets/css/bootstrap.min.css'
import './assets/css/elegant-icons.css'
import './assets/css/font-awesome.min.css'
import './assets/css/magnific-popup.css'
import './assets/css/slicknav.min.css'
import './assets/css/style.css'
import store from './store/store.ts'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { AuthProvider } from './context/AuthContext.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <AuthProvider>
        <App />
        <ToastContainer aria-label={undefined} />
      </AuthProvider>
    </Provider>
  </StrictMode>,
)
