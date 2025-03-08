import { FormControl, IconButton, InputLabel, List, ListItem, MenuItem, Select, SelectChangeEvent } from "@mui/material";
import { useEffect, useState } from "react";
import AccordionItem from "../../../../components/accordion/AccordionItem";
import mainCategoryApi from "../../../../services/api/mainCategoryApi";
import sizeApi from "../../../../services/api/sizeApi";
import colorApi from "../../../../services/api/colorApi";
import { MainCategory } from "../../../../models/entities/MainCategory";
import { Size } from "../../../../models/entities/Size";
import { Color } from "../../../../models/entities/Color";
import productApi from "../../../../services/api/productApi";
import { PagedProductDtos } from "../../../../models/dtos/Product/PagedProductDto";
import Pagination from "../../../../components/pagination/Pagination";
import { Link } from "react-router";
import { RootState } from "../../../../store/store";
import { useDispatch, useSelector } from "react-redux";
import Favorite from "@mui/icons-material/Favorite";
import { FavoriteBorder } from "@mui/icons-material";
import { toggleWishList } from "../../../../store/wishListSlice";

const ShopSection = () => {
    const [selectedSort, setSelectedSort] = useState("new-arrivals");
    const handleSort = (event: SelectChangeEvent<string>) => {
        setSelectedSort(event.target.value);
    };

    const [activeSize, setActiveSize] = useState<string | null>(null);
    const handleSizeActive = (size: string) => {
        setActiveSize(size);
    }
    
    const [mainCategories, setMainCategories] = useState<MainCategory[]>([]);
    const [sizes, setSizes] = useState<Size[]>([]);
    const [colors, setColors] = useState<Color[]>([]);
    useEffect(() => {
        const fetchMainCategories = async () => {
            try 
            {
                const response = await mainCategoryApi.getAll();    
                setMainCategories(response.data);
            }
            catch (error) 
            {
                console.error("Error:", error);
            }
        };

        const fetchSizes = async () => {
            try 
            {
                const response = await sizeApi.getAll();
                setSizes(response.data);
            }
            catch (error)
            {
                console.error("Error:", error);
            }
        };

        const fetchColors = async () => {
            try 
            {
                const response = await colorApi.getAll();
                setColors(response.data);
            }
            catch (error)
            {
                console.error("Error:", error);
            }
        };

        fetchMainCategories();
        fetchSizes();
        fetchColors();
    }, []);

    const [products, setProducts] = useState<PagedProductDtos[]>([]);
    const [page, setPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(12);
    const [totalRecords, setTotalRecords] = useState<number>(0)
    useEffect(() => {
        const fetchProducts = async () => {
            try
            {
                const response = await productApi.getPaged();
                const { items, totalRecords } = response.data;

                setProducts(items);
                setTotalRecords(totalRecords);
            }
            catch (error)
            {
                console.error("Error:", error);
            }
        };

        fetchProducts();
    }, [page]);
    
    const startIndex = (page - 1) * pageSize + 1;
    const endIndex = Math.min(page * pageSize, totalRecords);
    const totalPages = Math.max(1, Math.ceil(totalRecords / pageSize));
    const handlePageClick = (pageNumber: number) => {
        if (pageNumber >= 1 && pageNumber <= totalPages) {
            setPage(pageNumber);
        }
    };

    useEffect(() => {
        const elements = document.querySelectorAll(".set-bg");
        elements.forEach((el) => {
            const element = el as HTMLElement;
            const bg = element.dataset.setbg;
            if (bg) {
                element.style.backgroundImage = `url(${bg})`;
            }
        });
    }, [products]);

    const dispatch = useDispatch();
    const wishLists = useSelector((state: RootState) => state.wishList);

    return (
    // Shop Section Begin
    <section className="shop spad">
        <div className="container">
            <div className="row">
                <div className="col-lg-3">
                    <div className="shop__sidebar">
                        <div className="shop__sidebar__search">
                            <form action="#">
                                <input type="text" placeholder="Search..."/>
                                <button type="submit"><span className="icon_search"></span></button>
                            </form>
                        </div>
                        <div className="shop__sidebar__accordion">
                            {/* Category */}
                            <AccordionItem title="Categories" id="category-accordion">
                                <div className="shop__sidebar__categories">
                                    <List sx={{ listStyle: "unset", margin: 0, padding: 0, position: "unset", paddingTop: 0, paddingBottom: 0, maxHeight: '300px', overflowY: 'auto' }}>
                                        {mainCategories.map((category) => (
                                                <ListItem key={category.id} component="li" className="category-item">
                                                    <a href={`#${category.slug}`} className="category-link">{category.name}</a>
                                                </ListItem>
                                        ))}
                                    </List>
                                </div>
                            </AccordionItem>
                            
                            {/* Size */}
                            <AccordionItem title="Size" id="size-accordion">
                                <div className="shop__sidebar__size">
                                    {sizes.map((size) => (
                                        <label key={size.id} htmlFor={size.name} className={activeSize === size.name ? 'active' : ''}>
                                            {size.name}
                                            <input 
                                                type="radio" id={size.name} name="size" value={size.id}
                                                checked={activeSize === size.name} onChange={() => handleSizeActive(size.name)}
                                            />
                                        </label>
                                    ))}
                                </div>
                            </AccordionItem>

                            {/* Color */}
                            <AccordionItem title="Color" id="color-accordion">
                                <div className="shop__sidebar__color">
                                    {colors.map((color) => (
                                        <label key={color.id} htmlFor={color.name} style={{background: color.colorCode}}>
                                            <input 
                                                type="radio" id={color.name} name="color" value={color.id}
                                                
                                            />
                                        </label>
                                    ))}
                                </div>
                            </AccordionItem>
                            {/* <div className="accordion" id="accordionExample">
                                <div className="card">
                                    <div className="card-heading">
                                        <a data-toggle="collapse" data-target="#collapseThree">Filter Price</a>
                                    </div>
                                    <div id="collapseThree" className="collapse show" data-parent="#accordionExample">
                                        <div className="card-body">
                                            <div className="shop__sidebar__price">
                                                <ul>
                                                    <li><a href="#">$0.00 - $50.00</a></li>
                                                    <li><a href="#">$50.00 - $100.00</a></li>
                                                    <li><a href="#">$100.00 - $150.00</a></li>
                                                    <li><a href="#">$150.00 - $200.00</a></li>
                                                    <li><a href="#">$200.00 - $250.00</a></li>
                                                    <li><a href="#">250.00+</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="card">
                                    <div className="card-heading">
                                        <a data-toggle="collapse" data-target="#collapseFive">Colors</a>
                                    </div>
                                    <div id="collapseFive" className="collapse show" data-parent="#accordionExample">
                                        <div className="card-body">
                                            <div className="shop__sidebar__color">
                                                <label className="c-1" htmlFor="sp-1">
                                                    <input type="radio" id="sp-1"/>
                                                </label>
                                                <label className="c-2" htmlFor="sp-2">
                                                    <input type="radio" id="sp-2"/>
                                                </label>
                                                <label className="c-3" htmlFor="sp-3">
                                                    <input type="radio" id="sp-3"/>
                                                </label>
                                                <label className="c-4" htmlFor="sp-4">
                                                    <input type="radio" id="sp-4"/>
                                                </label>
                                                <label className="c-5" htmlFor="sp-5">
                                                    <input type="radio" id="sp-5"/>
                                                </label>
                                                <label className="c-6" htmlFor="sp-6">
                                                    <input type="radio" id="sp-6"/>
                                                </label>
                                                <label className="c-7" htmlFor="sp-7">
                                                    <input type="radio" id="sp-7"/>
                                                </label>
                                                <label className="c-8" htmlFor="sp-8">
                                                    <input type="radio" id="sp-8"/>
                                                </label>
                                                <label className="c-9" htmlFor="sp-9">
                                                    <input type="radio" id="sp-9"/>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="card">
                                    <div className="card-heading">
                                        <a data-toggle="collapse" data-target="#collapseSix">Tags</a>
                                    </div>
                                    <div id="collapseSix" className="collapse show" data-parent="#accordionExample">
                                        <div className="card-body">
                                            <div className="shop__sidebar__tags">
                                                <a href="#">Product</a>
                                                <a href="#">Bags</a>
                                                <a href="#">Shoes</a>
                                                <a href="#">Fashio</a>
                                                <a href="#">Clothing</a>
                                                <a href="#">Hats</a>
                                                <a href="#">Accessories</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div> */}
                        </div>
                    </div>
                </div>
                <div className="col-lg-9">
                    <div className="shop__product__option">
                        <div className="row">
                            <div className="col-lg-6 col-md-6 col-sm-6">
                                <div className="shop__product__option__left">
                                    <p>Showing {startIndex}–{endIndex} of {totalRecords} results</p>
                                </div>
                            </div>
                            <div className="col-lg-6 col-md-6 col-sm-6">
                                <div className="shop__product__option__right">
                                    {/* Sort */}
                                    <FormControl
                                        variant="outlined"
                                        size="small"
                                    >
                                        <InputLabel id="selected-sort-label">Sort by</InputLabel>
                                        <Select
                                            variant="outlined"
                                            value={selectedSort}
                                            onChange={handleSort}
                                            labelId="selected-sort-label"
                                            label={"Sort by"}
                                        >
                                            <MenuItem value="new-arrivals">New Arrivals</MenuItem>
                                            <MenuItem value="low-to-high">Low to High</MenuItem>
                                            <MenuItem value="high-to-low">High to Low</MenuItem>   
                                        </Select>
                                    </FormControl>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        {/* Products */}
                        {products.map((product) => {
                            const isWishList = wishLists.includes(product.id);

                            return (
                                <div className="col-lg-4 col-md-6 col-sm-6" key={product.id}>
                                <div className="product__item">
                                    <div className="product__item__pic set-bg" data-setbg="/src/assets/img/client/product/product-2.jpg">
                                    <ul className="product__hover">
                                        <li>
                                            <IconButton onClick={() => dispatch(toggleWishList(product.id))}>
                                                {isWishList ? <Favorite sx={{ color: "red" }} /> : <FavoriteBorder />}
                                            </IconButton>
                                        </li>
                                        <li>
                                        <a href="#"><img src="/src/assets/img/client/icon/compare.png" alt="" /> <span>Compare</span></a>
                                        </li>
                                        <li>
                                            <Link to={`/product/${product.id}`}>
                                                <img src="/src/assets/img/client/icon/search.png" alt="" />
                                            </Link>
                                        </li>
                                    </ul>
                                    </div>
                                    <div className="product__item__text">
                                    <h6>{product.name}</h6>
                                    <a href="#" className="add-cart">+ Add To Cart</a>
                                    <div className="rating">
                                        <i className="fa fa-star-o"></i>
                                        <i className="fa fa-star-o"></i>
                                        <i className="fa fa-star-o"></i>
                                        <i className="fa fa-star-o"></i>
                                        <i className="fa fa-star-o"></i>
                                    </div>
                                    <h5>${product.price}</h5>
                                    <div className="product__color__select">
                                        {product.colors.map((color) => (
                                        <label
                                            key={color.id}
                                            style={{
                                            background: color.colorCode,
                                            border: "1px solid black",
                                            }}
                                            htmlFor={`color-${color.id}`}
                                        >
                                            <input
                                            type="radio"
                                            id={`color-${color.id}`}
                                            name={`color-${product.id}`}
                                            value={color.id}
                                            />
                                        </label>
                                        ))}
                                    </div>
                                    </div>
                                </div>
                                </div>
                            );
                            })}
                        {/* <div className="col-lg-4 col-md-6 col-sm-6">
                            <div className="product__item sale">
                                <div className="product__item__pic set-bg" data-setbg="/src/assets/img/client/product/product-3.jpg">
                                    <span className="label">Sale</span>
                                    <ul className="product__hover">
                                        <li><a href="#"><img src="/src/assets/img/client/icon/heart.png" alt=""/></a></li>
                                        <li><a href="#"><img src="/src/assets/img/client/icon/compare.png" alt=""/> <span>Compare</span></a>
                                        </li>
                                        <li><a href="#"><img src="/src/assets/img/client/icon/search.png" alt=""/></a></li>
                                    </ul>
                                </div>
                                <div className="product__item__text">
                                    <h6>Multi-pocket Chest Bag</h6>
                                    <a href="#" className="add-cart">+ Add To Cart</a>
                                    <div className="rating">
                                        <i className="fa fa-star"></i>
                                        <i className="fa fa-star"></i>
                                        <i className="fa fa-star"></i>
                                        <i className="fa fa-star"></i>
                                        <i className="fa fa-star-o"></i>
                                    </div>
                                    <h5>$43.48</h5>
                                    <div className="product__color__select">
                                        <label htmlFor="pc-7">
                                            <input type="radio" id="pc-7"/>
                                        </label>
                                        <label className="active black" htmlFor="pc-8">
                                            <input type="radio" id="pc-8"/>
                                        </label>
                                        <label className="grey" htmlFor="pc-9">
                                            <input type="radio" id="pc-9"/>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div> */}
                    </div>
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="product__pagination">
                                <Pagination
                                    totalPages={totalPages}
                                    currentPage={page}
                                    onPageChange={handlePageClick}
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    // Shop Section End
  )
}

export default ShopSection