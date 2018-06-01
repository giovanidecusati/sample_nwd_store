import React, { Component } from 'react';

const products = {
  pageNumber: 1,
  pageSize: 10,
  totalNumberOfPages: 8,
  totalNumberOfRecords: 80,
  results: [
    {
      productId: 1,
      productName: 'Produto1',
      productPrice: 1.9,
      productFeatured: true,
      categoryName: 'Category',
    },
    {
      productId: 2,
      productName: 'Produto2',
      productPrice: 1.91,
      productFeatured: true,
      categoryName: 'Category',
    },
    {
      productId: 3,
      productName: 'Produto3',
      productPrice: 1.92,
      productFeatured: true,
      categoryName: 'Category',
    },
  ],
};

const ProductItem = props => {
  return (
    <tr>
      <td>{props.item.productId}</td>
      <td>{props.item.productName}</td>
      <td>{props.item.productPrice}</td>
      <td>{props.item.productFeatured ? 'true' : 'false'}</td>
      <td>{props.item.categoryName}</td>
      <td className="is-link is-icon">
        <a className="button is-success is-outlined is-pulled-right">
          <span>Edit</span>
          <span className="icon is-small">
            <i className="fa fa-pencil-square-o" />
          </span>
        </a>

        <a className="button is-danger is-outlined is-pulled-right">
          <span>Delete</span>
          <span className="icon is-small">
            <i className="fa fa-times" />
          </span>
        </a>
      </td>
    </tr>
  );
};

class ProductsPage extends Component {
  render() {
    return (
      <div>
        <section className="section">
          <div className="container">
            <nav className="panel">
              <p className="panel-heading">Products Search</p>
              <div className="panel-block">
                <form>
                  <div className="field is-grouped">
                    <p className="control is-expanded">
                      <label className="label">Name</label>
                      <div className="control">
                        <input className="input" type="text" />
                      </div>
                    </p>
                    <p className="control">
                      <a className="button is-info">Search</a>
                    </p>
                  </div>
                </form>
              </div>
            </nav>

            <table className="table is-bordered is-striped is-narrow">
              <thead>
                <tr>
                  <th>Id</th>
                  <th>Name</th>
                  <th>Price</th>
                  <th>Featured</th>
                  <th>Category</th>
                  <th>
                    <p className="has-text-centered">Actions</p>
                  </th>
                </tr>
              </thead>

              <tfoot>
                <tr>
                  <td colSpan="6">
                    <nav className="pagination is-centered">
                      <a className="pagination-previous">Previous</a>
                      <a className="pagination-next">Next page</a>
                      <ul className="pagination-list">
                        <li>
                          <a className="pagination-link">1</a>
                        </li>
                        <li>
                          <span className="pagination-ellipsis">&hellip;</span>
                        </li>
                        <li>
                          <a className="pagination-link">45</a>
                        </li>
                        <li>
                          <a className="pagination-link is-current">46</a>
                        </li>
                        <li>
                          <a className="pagination-link">47</a>
                        </li>
                        <li>
                          <span className="pagination-ellipsis">&hellip;</span>
                        </li>
                        <li>
                          <a className="pagination-link">86</a>
                        </li>
                      </ul>
                    </nav>
                  </td>
                </tr>
              </tfoot>

              <tbody>
                {products.results.map((item, i) => (
                  <ProductItem key={item.productId} item={item} />
                ))}
              </tbody>
            </table>
          </div>
        </section>
      </div>
    );
  }
}

export default ProductsPage;
