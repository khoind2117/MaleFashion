import { useDispatch } from "react-redux";
import { updateQuantity } from "../../store/cartSlice";

interface CartQuantitySelectorProps {
    productVariantId: number;
    quantity: number;
}

const CartQuantitySelector: React.FC<CartQuantitySelectorProps> = ({ productVariantId, quantity }) => {
    const dispatch = useDispatch();

    const handleQuantityChange = (newQuantity: number) => {
        dispatch(updateQuantity({ productVariantId, newQuantity }));
    }
    
    const handleIncrease = () => {
        const newQuantity = Math.min(quantity + 1, 10);
        handleQuantityChange(newQuantity);
    };

    const handleDecrease = () => {
        const newQuantity = Math.max(quantity - 1, 1); 
        handleQuantityChange(newQuantity);
    };

    return (
        <div className="quantity">
            <div className="pro-qty-2">
                <span className="fa fa-angle-left dec qtybtn" onClick={handleDecrease}/>
                <input type="text" value={quantity} min={1} max={10} readOnly/>
                <span className="fa fa-angle-right inc qtybtn" onClick={handleIncrease}/>
            </div>
        </div>
    );
}

export default CartQuantitySelector;