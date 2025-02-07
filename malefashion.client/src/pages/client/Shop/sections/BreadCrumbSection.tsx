import { Link } from 'react-router-dom'

const BreadCrumbSection = () => {
  return (
    // Breadcrumb Section Begin
    <section className="breadcrumb-option">
        <div className="container">
            <div className="row">
                <div className="col-lg-12">
                    <div className="breadcrumb__text">
                        <h4>Shop</h4>
                        <div className="breadcrumb__links">
                            <Link to="/">Home</Link>
                            <span>Shop</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    // Breadcrumb Section End
  )
}

export default BreadCrumbSection