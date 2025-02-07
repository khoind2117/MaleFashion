import CartQuantitySelector from '../../../../components/ui/CartQuantitySelector';
import { useDispatch, useSelector } from 'react-redux';
import { removeFromCart } from '../../../../store/cartSlice';
import { CartItem } from '../../../../models/dtos/CartItem/CartItem';
import { Link } from 'react-router-dom';
import { RootState } from '../../../../store/store';

const CartSection = () => {
    const dispatch = useDispatch();

    const cartItems = useSelector((state: RootState) => state.cart.items);

    const handleRemoveItem = (productVariantId: number) => {
        dispatch(removeFromCart(productVariantId));
    };

    const totalQuantity = cartItems.reduce((sum, item) => sum + item.quantity, 0);
    const subTotal = cartItems.reduce((sum, item) => sum + item.unitPrice * item.quantity, 0);
    const total = subTotal // Add shipping fees or taxes here if applicable
    return (
        // Shopping Cart Section Begin
        <section className="shopping-cart spad">
            <div className="container">
                <div className="row">
                    <div className="col-lg-8">
                        <div className="shopping__cart__table">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Quantity</th>
                                        <th>Total</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {cartItems.length === 0 ? (
                                        <tr>
                                            <td colSpan={4}>Your cart is currently empty.</td>
                                        </tr>
                                        ) : (
                                            cartItems.map((item: CartItem) => (
                                                <tr key={item.productVariantId}>
                                                    <td className="product__cart__item d-flex">
                                                        <div 
                                                            className="product__cart__item__pic"
                                                            style={{ maxWidth: "150px", maxHeight: "150px", flexShrink: 0 }}
                                                        >
                                                            <img 
                                                                src="/src/assets/img/client/product/product-2.jpg" 
                                                                alt=""
                                                                style={{ width: "100%", height: "100%", objectFit: "cover" }} 
                                                            />
                                                        </div>
                                                        <div className="product__cart__item__text d-flex flex-column justify-content-between">
                                                            <Link to={`/product/${item.productVariantDto.productId}`}>
                                                                <h5>{item.name}</h5>
                                                            </Link>
                                                            <div>                                                         
                                                                <h6>Color: {item.productVariantDto.colorDto.name}</h6>
                                                                <h6>Size: {item.productVariantDto.sizeDto.name}</h6>
                                                                <h5>${item.unitPrice.toFixed(2)}</h5>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td className="quantity__item">
                                                        <CartQuantitySelector
                                                            productVariantId={item.productVariantId}
                                                            quantity={item.quantity}
                                                        />
                                                    </td>
                                                    <td className="cart__price">${(item.unitPrice * item.quantity).toFixed(2)}</td>
                                                    <td className="cart__close">
                                                        <i
                                                            className="fa fa-close"
                                                            onClick={() => handleRemoveItem(item.productVariantId)}
                                                        ></i>
                                                    </td>
                                                </tr>
                                            ))
                                        )                                    
                                    }
                                </tbody>
                            </table>
                        </div>
                        <div className="row">
                            <div className="col-lg-6 col-md-6 col-sm-6">
                                <div className="continue__btn">
                                    <Link to="/shop">Continue Shopping</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-4">
                        <div className="cart__discount">
                            <h6>Discount codes</h6>
                            <form action="#">
                                <input type="text" placeholder="Coupon code"/>
                                <button type="submit">Apply</button>
                            </form>
                        </div>
                        <div className="cart__total">
                            <h6>Cart summary</h6>
                            <ul>
                                <li>Item(s) in Cart <span>{cartItems.length}</span></li>
                                <li>Total Quantity <span>{totalQuantity}</span></li>
                                <li>Subtotal <span>$ {subTotal.toFixed(2)}</span></li>
                                <li>Total <span>$ {total.toFixed(2)}</span></li>
                            </ul>
                            <Link to="/checkout" className="primary-btn">
                                Proceed to checkout
                            </Link>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        // Shopping Cart Section End
    )
}

export default CartSection