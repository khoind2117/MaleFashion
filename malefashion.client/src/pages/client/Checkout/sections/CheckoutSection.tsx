import { useDispatch, useSelector } from "react-redux"
import { RootState } from "../../../../store/store";
import { useCallback, useEffect, useState } from "react";
import accountApi from "../../../../services/api/accountApi";
import { toast } from "react-toastify";
import orderApi from "../../../../services/api/orderApi";
import { OrderRequestDto } from "../../../../models/dtos/Order/OrderRequestDto";
import { setCartFromAPI } from "../../../../store/cartSlice";
import cartApi from "../../../../services/api/cartApi";

const CheckoutSection = () => {
    const cartItems = useSelector((state: RootState) => state.cart.items);
    const dispatch = useDispatch();
    const [autoFill, setAutoFill] = useState(false);
    const [orderRequestDto, setOrderRequestDto] = useState<OrderRequestDto>({
        firstName: "",
        lastName: "",
        address: "",
        phoneNumber: "",
        email: "",
        note: "",
        paymentMethod: "COD",
    });

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const fetchUserInfo = useCallback(async () => {
        try {
            const response = await accountApi.getProfile();
            setOrderRequestDto(prevState => ({
                ...prevState,
                firstName: response.data.firstName ?? prevState.firstName,
                lastName: response.data.lastName ?? prevState.lastName,
                address: response.data.address ?? prevState.address,
                phoneNumber: response.data.phoneNumber ?? prevState.phoneNumber,
                email: response.data.email ?? prevState.email,
            }));                
        } catch (error) {
            console.error("Error fetching user info:", error);
        } 
    }, []);

    const fetchCartFromAPI = useCallback(async () => {
        try {
            const response = await cartApi.getCart();
            dispatch(setCartFromAPI(response.data));
            toast.success("Fetch cart from API successfully");
        } catch (error) {
            console.error("Error fetching cart from API:", error);
        }
    }, [dispatch]);
    
    useEffect(() => {
        fetchCartFromAPI();
    }, [fetchCartFromAPI]);

    useEffect(() => {
        if (autoFill) {
            fetchUserInfo();
        } else {
            setOrderRequestDto(prevState => ({ 
                firstName: "",
                lastName: "",
                address: "",
                phoneNumber: "",
                email: "",
                note: prevState.note,
                paymentMethod: prevState.paymentMethod,
            }));
        }
    }, [autoFill, fetchUserInfo]);
    
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setOrderRequestDto(prevState => ({
            ...prevState,
            [name]: value,
        }));
    };

    const handlePaymentMethodChange = (method: "COD" | "PayPal") => {
        setOrderRequestDto(prevState => ({
            ...prevState,
            paymentMethod: method,
        }));
    };
    
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!orderRequestDto.firstName || !orderRequestDto.lastName || !orderRequestDto.address || !orderRequestDto.phoneNumber || !orderRequestDto.email || !orderRequestDto.paymentMethod) {
            toast.warning("Please fill in all required fields.");
            return;
        }

        if (isSubmitting) return;

        setIsSubmitting(true);

        try {
            const response = await orderApi.checkOut(orderRequestDto);
            if (response.status === 200) {
                toast.success("Checkout successful!");
                // Xử lý sau khi checkout thành công
            } else {
                toast.error("Failed to complete checkout.");
            }
        } catch (error: unknown) {
            setError("An error occurred during checkout. Please try again.");
            console.error(error);
    
            if (error instanceof Error) {
                toast.error(error.message || "An error occurred during checkout.");
            } else {
                toast.error("An unknown error occurred.");
            }
        } finally {
            setIsSubmitting(false);
        }
    };

    const subTotal = cartItems.reduce((sum, item) => sum + item.productVariantDto.productDto.price * item.quantity, 0);
    const total = subTotal

    return (
        // Checkout Section Begin
        <section className="checkout spad">
            <div className="container">
                <div className="checkout__form">
                    <form onSubmit={(e) => handleSubmit(e)}>
                        <div className="row">
                            <div className="col-lg-7 col-md-6">
                                <h6 className="coupon__code">
                                    <span className="icon_tag_alt"></span>
                                    Have a coupon? <a href="#">Click here</a> to enter your code
                                </h6>
                                <h6 className="checkout__title">Billing Details</h6>
                                <div className="checkout__input__checkbox">
                                    <label htmlFor="auto-fill">
                                        Auto-fill with personal information
                                        <input 
                                            type="checkbox"
                                            id="auto-fill"
                                            checked={autoFill}
                                            onChange={(e) => setAutoFill(e.target.checked)}
                                        />
                                        <span className="checkmark"></span>
                                    </label>
                                </div>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Fist Name<span>*</span></p>
                                            <input 
                                                type="text"
                                                name="firstName"
                                                value={orderRequestDto.firstName}
                                                onChange={handleInputChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Last Name<span>*</span></p>
                                            <input 
                                                type="text"
                                                name="lastName"
                                                value={orderRequestDto.lastName}
                                                onChange={handleInputChange}
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="checkout__input">
                                    <p>Address<span>*</span></p>
                                    <input 
                                        type="text"
                                        name="address"
                                        value={orderRequestDto.address}
                                        onChange={handleInputChange}
                                        className="checkout__input__add"
                                    />
                                </div>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>PhoneNumber<span>*</span></p>
                                            <input 
                                                type="text"
                                                name="phoneNumber"
                                                value={orderRequestDto.phoneNumber}
                                                onChange={handleInputChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Email<span>*</span></p>
                                            <input 
                                                type="text"
                                                name="email"
                                                value={orderRequestDto.email}
                                                onChange={handleInputChange}
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="checkout__input">
                                    <p>Order notes</p>
                                    <input 
                                        type="text"
                                        name="note"
                                        value={orderRequestDto.note}
                                        onChange={handleInputChange}
                                        placeholder="Notes about your order, e.g. special notes for delivery."
                                    />
                                </div>
                            </div>
                            <div className="col-lg-5 col-md-6">
                                <div className="checkout__order">
                                    <h4 className="order__title">Your order</h4>
                                    <div className="checkout__order__products">Product <span>Total</span></div>
                                    <ul className="checkout__total__products">
                                        {cartItems.map((item, index) => (
                                            <li key={index} className="row mr-0 ml-0">
                                                <div className="col-8 p-0">
                                                    {String(index + 1).padStart(2, "0")}. {item.productVariantDto.productDto.name}
                                                </div>
                                                <span className="col-4 p-0" style={{textAlign: "end"}}>
                                                    ${(item.productVariantDto.productDto.price * item.quantity).toFixed(2)}
                                                </span>
                                            </li>
                                        ))}
                                    </ul>
                                    <ul className="checkout__total__all">
                                        <li>Subtotal <span>$ {subTotal.toFixed(2)}</span></li>
                                        <li>Total <span>$ {total.toFixed(2)}</span></li>
                                    </ul>
                                    <p>
                                        By placing an order, you agree to our <a href="#">terms and conditions</a>.
                                        Please ensure your shipping details are correct before proceeding.
                                    </p>
                                    <div className="checkout__input__checkbox">
                                        <label htmlFor="payment">
                                            COD (Cash On Delivery)
                                            <input 
                                                type="radio" 
                                                id="payment" 
                                                name="paymentMethod" 
                                                value="COD" 
                                                checked={orderRequestDto.paymentMethod === "COD"} 
                                                onChange={() => handlePaymentMethodChange("COD")} 
                                            />
                                            <span className="checkmark"></span>
                                        </label>
                                    </div>

                                    <div className="checkout__input__checkbox">
                                        <label htmlFor="paypal">
                                            Paypal
                                            <input 
                                                type="radio" 
                                                id="paypal" 
                                                name="paymentMethod" 
                                                value="PayPal" 
                                                checked={orderRequestDto.paymentMethod === "PayPal"} 
                                                onChange={() => handlePaymentMethodChange("PayPal")} 
                                            />
                                            <span className="checkmark"></span>
                                        </label>
                                    </div>

                                    <button type="submit" disabled={isSubmitting} className="site-btn">
                                        {isSubmitting ? "Processing..." : "Checkout"}
                                    </button>
                                    {error && <div className="error-message">{error}</div>}
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </section>
        // Checkout Section End
    )
}

export default CheckoutSection