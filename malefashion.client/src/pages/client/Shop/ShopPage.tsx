import MainLayout from '../../../layouts/MainLayout'
import BreadCrumbSection from './sections/BreadCrumbSection'
import ShopSection from './sections/ShopSection'

const ShopPage = () => {
  return (
    <>
      <MainLayout>
        <BreadCrumbSection/>
        <ShopSection/>
      </MainLayout>
    </>
  )
}

export default ShopPage