import MainLayout from "../../../layouts/MainLayout";
import BreadCrumbSection from "../Shop/sections/BreadCrumbSection";
import CartSection from "./sections/CartSection";

const CartPage = () => {    
    return (
        <MainLayout>
            <BreadCrumbSection/>
            <CartSection/>
        </MainLayout>
    )
}

export default CartPage