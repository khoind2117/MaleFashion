interface ProductQuantitySelectorProps {
    quantity: number;
    onQuantityChange: (newQuantity: number) => void;
}

const ProductQuantitySelector: React.FC<ProductQuantitySelectorProps> = ({quantity, onQuantityChange}) => {
    const handleIncrease = () => {
        const newQuantity = Math.min(quantity + 1, 10);
        onQuantityChange(newQuantity);
    };

    const handleDecrease = () => {
        const newQuantity = Math.max(quantity - 1, 1); 
        onQuantityChange(newQuantity);
    };

    const handleQuantityChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newQuantity = event?.target.value;
        const numericQuantity = Number(newQuantity);
        
        if (!isNaN(numericQuantity)) {
            const clampedQuantity = Math.max(1, Math.min(10, numericQuantity));
            onQuantityChange(clampedQuantity);
        } else {
            onQuantityChange(1); 
        }
    };

    return (
        <div className="quantity">
            <div className="pro-qty">
                <span className="fa fa-angle-up inc qtybtn" onClick={handleIncrease}/>
                <input type="text" value={quantity} onChange={handleQuantityChange} min={1} max={10} />
                <span className="fa fa-angle-down dec qtybtn" onClick={handleDecrease}/>
            </div>
        </div>
    );
}

export default ProductQuantitySelector;