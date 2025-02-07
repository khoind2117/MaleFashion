import MainLayout from '../../../layouts/MainLayout'
import BreadCrumbSection from '../Shop/sections/BreadCrumbSection'
import CheckoutSection from './sections/CheckoutSection'

const CheckoutPage = () => {
  return (
    <MainLayout>
        <BreadCrumbSection/>
        <CheckoutSection/>
    </MainLayout>
  )
}

export default CheckoutPage