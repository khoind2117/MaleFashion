import { Link } from 'react-router'
import ProductGallery from '../../../../components/ui/ProductGallery';
import { useParams } from 'react-router';
import { useEffect, useState } from 'react';
import productApi from '../../../../services/api/productApi';
import { ProductDetailDto } from '../../../../models/dtos/Product/ProductDetailDto';
import { addToCart } from '../../../../store/cartSlice';
import { useDispatch } from 'react-redux';
import { CartItem } from '../../../../models/dtos/CartItem/CartItem';
import ProductQuantitySelector from '../../../../components/ui/ProductQuantitySelector';

const ShopDetailsSection = () => {
    const params = useParams();
    const productId = Number(params.productId);

    const [product, setProduct] = useState<ProductDetailDto | null>(null);
    const [selectedColorId, setSelectedColorId] = useState<number | null>(null);
    const [activeSize, setActiveSize] = useState<number | null>(null);
    const [quantity, setQuantity] = useState(1);

    useEffect(() => {
        const fetchProduct = async () => {
            try {
                const response = await productApi.getById(productId);
                setProduct(response.data);

                if (response.data.productVariantDtos.length > 0) {
                    setSelectedColorId(response.data.productVariantDtos[0].colorDto.id);
                }
            } catch (error) {
                console.error("Error fetching product:", error);
            }
        };

        fetchProduct();
    }, [productId]);

    const uniqueColors = Array.from(
        new Map(
            product?.productVariantDtos.map((variant) => [
                variant.colorDto.id,
                {
                    colorId: variant.colorDto.id,
                    colorName: variant.colorDto.name,
                    colorCode: variant.colorDto.colorCode,
                },
            ])
        ).values()
    );

    const sizesForSelectedColor =
        product?.productVariantDtos
            .filter((variant) => variant.colorDto.id === selectedColorId)
            .map((variant) => ({
                sizeId: variant.sizeDto.id,
                sizeName: variant.sizeDto.name,
            })) ?? [];

    const handleColorChange = (colorId: number) => {
        setSelectedColorId(colorId);
        setActiveSize(null);
    };

    const handleSizeSelection = (sizeId: number) => {
        setActiveSize(sizeId);
    };

    const dispatch = useDispatch();
    const handleAddToCart = () => {
        if (!selectedColorId || !activeSize) {
            alert("Vui lòng chọn màu sắc và kích thước!");
            return;
        }
    
        const selectedProductVariant = product?.productVariantDtos.find(
            (variant) => variant.colorDto.id === selectedColorId && variant.sizeDto.id === activeSize
        );
    
        if (!selectedProductVariant) {
            alert("Sản phẩm không có biến thể phù hợp!");
            return;
        }
    
        const cartItem: CartItem = {
            name: product?.name || "",
            quantity: quantity,
            unitPrice: product?.price || 0,
            productVariantId: selectedProductVariant.id,
            productVariantDto: selectedProductVariant
        };
    
        dispatch(addToCart(cartItem));
    };

    return (
        // Shop Details Section Begin
        <section className="shop-details">
        <div className="product__details__pic">
            <div className="container">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="product__details__breadcrumb">
                            <Link to={"/"}>Home</Link>
                            <Link to={"/shop"}>Shop</Link>
                            <span>Product Details</span>
                        </div>
                    </div>
                </div>
                <ProductGallery/>
            </div>
        </div>
        <div className="product__details__content">
            <div className="container">
                <div className="row d-flex justify-content-center">
                    <div className="col-lg-8">
                        <div className="product__details__text">
                            <h4>{product?.name}</h4>
                            <div className="rating">
                                <i className="fa fa-star"></i>
                                <i className="fa fa-star"></i>
                                <i className="fa fa-star"></i>
                                <i className="fa fa-star"></i>
                                <i className="fa fa-star-o"></i>
                                <span> - 5 Reviews</span>
                            </div>
                            {/* <h3>$270.00 <span>70.00</span></h3> */}
                            <h3>${product?.price.toFixed(2)}</h3>
                            <p>{product?.description}</p>
                            
                            <div className="product__details__option">
                                <div className="product__details__option__color">
                                    <span>Color:</span>
                                    {uniqueColors.map((color) => (
                                        <label key={color.colorId} className={color.colorId === selectedColorId ? "active" : ""} style={{ backgroundColor: color.colorCode }}>
                                        <input
                                            type="radio"
                                            name="color"
                                            value={color.colorId}
                                            checked={color.colorId === selectedColorId}
                                            onClick={() => handleColorChange(color.colorId)}
                                        />
                                        </label>
                                    ))}
                                </div>

                                <div className="product__details__option__size">
                                    <span>Size:</span>
                                    {sizesForSelectedColor.map((size) => (
                                        <label key={size.sizeId} htmlFor={size.sizeName} className={size.sizeId === activeSize ? "active" : ""}>
                                        {size.sizeName}
                                        <input 
                                            type="radio"
                                            id={size.sizeName}
                                            name="size"
                                            value={size.sizeId}
                                            checked={size.sizeId === activeSize}
                                            onChange={() => handleSizeSelection(size.sizeId)}
                                        />
                                        </label>
                                    ))}
                                </div>
                            </div>
                            <div className="product__details__cart__option">
                                <ProductQuantitySelector quantity={quantity} onQuantityChange={setQuantity} />
                                <button className="primary-btn" onClick={() => handleAddToCart()}>Add to Cart</button>
                            </div>
                            <div className="product__details__btns__option">
                                <a href="#"><i className="fa fa-heart"></i> add to wishlist</a>
                                <a href="#"><i className="fa fa-exchange"></i> Add To Compare</a>
                            </div>
                            <div className="product__details__last__option">
                                <h5><span>Guaranteed Safe Checkout</span></h5>
                                <img src="/src/assets/img/shop-details/details-payment.png" alt=""/>
                                <ul>
                                    <li><span>SKU:</span> 3812912</li>
                                    <li><span>Categories:</span> Clothes</li>
                                    <li><span>Tag:</span> Clothes, Skin, Body</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-lg-12">
                        <div className="product__details__tab">
                            <ul className="nav nav-tabs" role="tablist">
                                <li className="nav-item">
                                    <a className="nav-link active" data-toggle="tab" href="#tabs-5"
                                    role="tab">Description</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" data-toggle="tab" href="#tabs-6" role="tab">Customer
                                    Previews(5)</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" data-toggle="tab" href="#tabs-7" role="tab">Additional
                                    information</a>
                                </li>
                            </ul>
                            <div className="tab-content">
                                <div className="tab-pane active" id="tabs-5" role="tabpanel">
                                    <div className="product__details__tab__content">
                                        <p className="note">Nam tempus turpis at metus scelerisque placerat nulla deumantos
                                            solicitud felis. Pellentesque diam dolor, elementum etos lobortis des mollis
                                            ut risus. Sedcus faucibus an sullamcorper mattis drostique des commodo
                                        pharetras loremos.</p>
                                        <div className="product__details__tab__content__item">
                                            <h5>Products Infomation</h5>
                                            <p>A Pocket PC is a handheld computer, which features many of the same
                                                capabilities as a modern PC. These handy little devices allow
                                                individuals to retrieve and store e-mail messages, create a contact
                                                file, coordinate appointments, surf the internet, exchange text messages
                                                and more. Every product that is labeled as a Pocket PC must be
                                                accompanied with specific software to operate the unit and must feature
                                            a touchscreen and touchpad.</p>
                                            <p>As is the case with any new technology product, the cost of a Pocket PC
                                                was substantial during it’s early release. htmlFor approximately $700.00,
                                                consumers could purchase one of top-of-the-line Pocket PCs in 2003.
                                                These days, customers are finding that prices have become much more
                                                reasonable now that the newness is wearing off. htmlFor approximately
                                            $350.00, a new Pocket PC can now be purchased.</p>
                                        </div>
                                        <div className="product__details__tab__content__item">
                                            <h5>Material used</h5>
                                            <p>Polyester is deemed lower quality due to its none natural quality’s. Made
                                                from synthetic materials, not natural like wool. Polyester suits become
                                                creased easily and are known htmlFor not being breathable. Polyester suits
                                                tend to have a shine to them compared to wool and cotton suits, this can
                                                make the suit look cheap. The texture of velvet is luxurious and
                                                breathable. Velvet is a great choice htmlFor dinner party jacket and can be
                                            worn all year round.</p>
                                        </div>
                                    </div>
                                </div>
                                <div className="tab-pane" id="tabs-6" role="tabpanel">
                                    <div className="product__details__tab__content">
                                        <div className="product__details__tab__content__item">
                                            <h5>Products Infomation</h5>
                                            <p>A Pocket PC is a handheld computer, which features many of the same
                                                capabilities as a modern PC. These handy little devices allow
                                                individuals to retrieve and store e-mail messages, create a contact
                                                file, coordinate appointments, surf the internet, exchange text messages
                                                and more. Every product that is labeled as a Pocket PC must be
                                                accompanied with specific software to operate the unit and must feature
                                            a touchscreen and touchpad.</p>
                                            <p>As is the case with any new technology product, the cost of a Pocket PC
                                                was substantial during it’s early release. htmlFor approximately $700.00,
                                                consumers could purchase one of top-of-the-line Pocket PCs in 2003.
                                                These days, customers are finding that prices have become much more
                                                reasonable now that the newness is wearing off. htmlFor approximately
                                            $350.00, a new Pocket PC can now be purchased.</p>
                                        </div>
                                        <div className="product__details__tab__content__item">
                                            <h5>Material used</h5>
                                            <p>Polyester is deemed lower quality due to its none natural quality’s. Made
                                                from synthetic materials, not natural like wool. Polyester suits become
                                                creased easily and are known htmlFor not being breathable. Polyester suits
                                                tend to have a shine to them compared to wool and cotton suits, this can
                                                make the suit look cheap. The texture of velvet is luxurious and
                                                breathable. Velvet is a great choice htmlFor dinner party jacket and can be
                                            worn all year round.</p>
                                        </div>
                                    </div>
                                </div>
                                <div className="tab-pane" id="tabs-7" role="tabpanel">
                                    <div className="product__details__tab__content">
                                        <p className="note">Nam tempus turpis at metus scelerisque placerat nulla deumantos
                                            solicitud felis. Pellentesque diam dolor, elementum etos lobortis des mollis
                                            ut risus. Sedcus faucibus an sullamcorper mattis drostique des commodo
                                        pharetras loremos.</p>
                                        <div className="product__details__tab__content__item">
                                            <h5>Products Infomation</h5>
                                            <p>A Pocket PC is a handheld computer, which features many of the same
                                                capabilities as a modern PC. These handy little devices allow
                                                individuals to retrieve and store e-mail messages, create a contact
                                                file, coordinate appointments, surf the internet, exchange text messages
                                                and more. Every product that is labeled as a Pocket PC must be
                                                accompanied with specific software to operate the unit and must feature
                                            a touchscreen and touchpad.</p>
                                            <p>As is the case with any new technology product, the cost of a Pocket PC
                                                was substantial during it’s early release. htmlFor approximately $700.00,
                                                consumers could purchase one of top-of-the-line Pocket PCs in 2003.
                                                These days, customers are finding that prices have become much more
                                                reasonable now that the newness is wearing off. htmlFor approximately
                                            $350.00, a new Pocket PC can now be purchased.</p>
                                        </div>
                                        <div className="product__details__tab__content__item">
                                            <h5>Material used</h5>
                                            <p>Polyester is deemed lower quality due to its none natural quality’s. Made
                                                from synthetic materials, not natural like wool. Polyester suits become
                                                creased easily and are known htmlFor not being breathable. Polyester suits
                                                tend to have a shine to them compared to wool and cotton suits, this can
                                                make the suit look cheap. The texture of velvet is luxurious and
                                                breathable. Velvet is a great choice htmlFor dinner party jacket and can be
                                            worn all year round.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    // Shop Details Section End    
  )
}

export default ShopDetailsSection