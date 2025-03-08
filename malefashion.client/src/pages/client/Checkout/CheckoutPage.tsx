import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import MainLayout from '../../../layouts/MainLayout'
import BreadCrumbSection from '../Shop/sections/BreadCrumbSection'
import CheckoutSection from './sections/CheckoutSection'
import { toast } from 'react-toastify';
import { useEffect } from 'react';

const CheckoutPage = () => {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
      if (!isAuthenticated) {
        toast.warning("You need to log in to proceed to checkout.");
        navigate('/login');
      }
  }, [isAuthenticated, navigate]);

  return (
    <MainLayout>
        <BreadCrumbSection/>
        <CheckoutSection/>
    </MainLayout>
  )
}

export default CheckoutPage