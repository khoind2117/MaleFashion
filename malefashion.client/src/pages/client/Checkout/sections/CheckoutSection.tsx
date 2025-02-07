import { useSelector } from "react-redux"
import { RootState } from "../../../../store/store";

const CheckoutSection = () => {
    const cartItems = useSelector((state: RootState) => state.cart.items);

    const subTotal = cartItems.reduce((sum, item) => sum + item.unitPrice * item.quantity, 0);
    const total = subTotal

    return (
        // Checkout Section Begin
        <section className="checkout spad">
            <div className="container">
                <div className="checkout__form">
                    <form action="#">
                        <div className="row">
                            <div className="col-lg-8 col-md-6">
                                <h6 className="coupon__code"><span className="icon_tag_alt"></span> Have a coupon? <a href="#">Click
                                here</a> to enter your code</h6>
                                <h6 className="checkout__title">Billing Details</h6>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Fist Name<span>*</span></p>
                                            <input type="text"/>
                                        </div>
                                    </div>
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Last Name<span>*</span></p>
                                            <input type="text"/>
                                        </div>
                                    </div>
                                </div>
                                <div className="checkout__input">
                                    <p>Country<span>*</span></p>
                                    <input type="text"/>
                                </div>
                                <div className="checkout__input">
                                    <p>Address<span>*</span></p>
                                    <input type="text" placeholder="Street Address" className="checkout__input__add"/>
                                    <input type="text" placeholder="Apartment, suite, unite ect (optinal)"/>
                                </div>
                                <div className="checkout__input">
                                    <p>Town/City<span>*</span></p>
                                    <input type="text"/>
                                </div>
                                <div className="checkout__input">
                                    <p>Country/State<span>*</span></p>
                                    <input type="text"/>
                                </div>
                                <div className="checkout__input">
                                    <p>Postcode / ZIP<span>*</span></p>
                                    <input type="text"/>
                                </div>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Phone<span>*</span></p>
                                            <input type="text"/>
                                        </div>
                                    </div>
                                    <div className="col-lg-6">
                                        <div className="checkout__input">
                                            <p>Email<span>*</span></p>
                                            <input type="text"/>
                                        </div>
                                    </div>
                                </div>
                                <div className="checkout__input__checkbox">
                                    <label htmlFor="acc">
                                        Create an account?
                                        <input type="checkbox" id="acc"/>
                                        <span className="checkmark"></span>
                                    </label>
                                    <p>Create an account by entering the information below. If you are a returning customer
                                    please login at the top of the page</p>
                                </div>
                                <div className="checkout__input">
                                    <p>Account Password<span>*</span></p>
                                    <input type="text"/>
                                </div>
                                <div className="checkout__input__checkbox">
                                    <label htmlFor="diff-acc">
                                        Note about your order, e.g, special noe htmlFor delivery
                                        <input type="checkbox" id="diff-acc"/>
                                        <span className="checkmark"></span>
                                    </label>
                                </div>
                                <div className="checkout__input">
                                    <p>Order notes<span>*</span></p>
                                    <input type="text"
                                    placeholder="Notes about your order, e.g. special notes htmlFor delivery."/>
                                </div>
                            </div>
                            <div className="col-lg-4 col-md-6">
                                <div className="checkout__order">
                                    <h4 className="order__title">Your order</h4>
                                    <div className="checkout__order__products">Product <span>Total</span></div>
                                    <ul className="checkout__total__products">
                                        {cartItems.map((item, index) => (
                                            <li key={index} className="row mr-0 ml-0">
                                                <div className="col-8 p-0">
                                                    {String(index + 1).padStart(2, "0")}. {item.name}
                                                </div>
                                                <span className="col-4 p-0" style={{textAlign: "end"}}>
                                                    ${(item.unitPrice * item.quantity).toFixed(2)}
                                                </span>
                                            </li>
                                        ))}
                                    </ul>
                                    <ul className="checkout__total__all">
                                        <li>Subtotal <span>$ {subTotal.toFixed(2)}</span></li>
                                        <li>Total <span>$ {total.toFixed(2)}</span></li>
                                    </ul>
                                    <div className="checkout__input__checkbox">
                                        <label htmlFor="acc-or">
                                            Create an account?
                                            <input type="checkbox" id="acc-or"/>
                                            <span className="checkmark"></span>
                                        </label>
                                    </div>
                                    <p>Lorem ipsum dolor sit amet, consectetur adip elit, sed do eiusmod tempor incididunt
                                    ut labore et dolore magna aliqua.</p>
                                    <div className="checkout__input__checkbox">
                                        <label htmlFor="payment">
                                            Check Payment
                                            <input type="checkbox" id="payment"/>
                                            <span className="checkmark"></span>
                                        </label>
                                    </div>
                                    <div className="checkout__input__checkbox">
                                        <label htmlFor="paypal">
                                            Paypal
                                            <input type="checkbox" id="paypal"/>
                                            <span className="checkmark"></span>
                                        </label>
                                    </div>
                                    <button type="submit" className="site-btn">PLACE ORDER</button>
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