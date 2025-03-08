import { useDispatch, useSelector } from 'react-redux';
import { mergeCart, removeFromCart, setCartFromAPI } from '../../../../store/cartSlice';
import { Link, useNavigate } from 'react-router-dom';
import { AppDispatch, RootState } from '../../../../store/store';
import { CartItemDto } from '../../../../models/dtos/CartItem/CartItemDto';
import { useEffect, useState } from 'react';
import cartApi from '../../../../services/api/cartApi';
import { toast } from 'react-toastify';
import { useAuth } from '../../../../context/AuthContext';

const CartSection = () => {
    const { isAuthenticated } = useAuth();
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const basketId = useSelector((state: RootState) => state.cart.basketId);
    const isMerged = useSelector((state: RootState) => state.cart.isMerged);
    const cartItems = useSelector((state: RootState) => state.cart.items);
    const [totalQuantity, setTotalQuantity] = useState(0);
    const [subTotal, setSubTotal] = useState(0);
    const [total, setTotal] = useState(0);

    useEffect(() => {
        const fetchCartFromAPI = async () => {
            try {
                const response = await cartApi.getCart();
                dispatch(setCartFromAPI(response.data));
                toast.success("Fetch cart from API successfully");
            } catch (error) {
                console.error("Error fetching cart from API:", error);
            }
        }
        const checkAndMergeCart = async () => {
            if (!isMerged && isAuthenticated() && basketId) {
                try {
                    dispatch(mergeCart());
                } catch (error) {
                    console.error("Error merging cart:", error);
                }
            }
            fetchCartFromAPI();
        }

        checkAndMergeCart();
    }, [basketId, dispatch, isAuthenticated, isMerged]);
  
    useEffect(() => {
      if (cartItems.length === 0) {
        setTotalQuantity(0);
        setSubTotal(0);
        setTotal(0);
        return;
      }
  
      const totalQuantity = cartItems.reduce((sum, item) => sum + item.quantity, 0);
      const subTotal = cartItems.reduce(
        (sum, item) => sum + item.productVariantDto.productDto.price * item.quantity, 0
      );
      setTotalQuantity(totalQuantity);
      setSubTotal(subTotal);
      setTotal(subTotal);
    }, [cartItems]);

    const handleRemoveItem = async (productVariantId: number) => {
        dispatch(removeFromCart(productVariantId));
    };

    const handleCheckout = () => {
        if (isAuthenticated()) {
            navigate("/checkout");
        } else {
            navigate("/login");
        }
    };

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
                                        cartItems.map((item: CartItemDto) => (
                                            <tr key={item.productVariantDto.id}>
                                                <td className="product__cart__item d-flex">
                                                    <div className="product__cart__item__pic" style={{ maxWidth: "150px", maxHeight: "150px", flexShrink: 0 }}>
                                                        <img src="/src/assets/img/client/product/product-2.jpg" alt="" style={{ width: "100%", height: "100%", objectFit: "cover" }} />
                                                    </div>
                                                    <div className="product__cart__item__text d-flex flex-column justify-content-between">
                                                        <a href={`/product/${item.productVariantDto.productDto.id}`}>
                                                            <h5>{item.productVariantDto.productDto?.name}</h5>
                                                        </a>
                                                        <div>
                                                            <h6>Color: {item.productVariantDto.colorDto?.name}</h6>
                                                            <h6>Size: {item.productVariantDto.sizeDto?.name}</h6>
                                                            <h5>${item.productVariantDto.productDto.price.toFixed(2)}</h5>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td className="quantity__item">
                                                    <div className="quantity">
                                                        <div className="pro-qty-2">
                                                            <input type="text" value={item.quantity} readOnly/>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td className="cart__price">${(item.productVariantDto.productDto.price * item.quantity).toFixed(2)}</td>
                                                <td className="cart__close">
                                                    <i className="fa fa-close" onClick={() => handleRemoveItem(item.productVariantDto.id)}></i>
                                                </td>
                                            </tr>
                                        ))
                                    )}
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
                            {cartItems.length > 0 && (
                                <button 
                                    onClick={handleCheckout} 
                                    className="primary-btn w-100"
                                >
                                    Proceed to checkout
                                </button>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </section>
        // Shopping Cart Section End
    )
}

export default CartSection