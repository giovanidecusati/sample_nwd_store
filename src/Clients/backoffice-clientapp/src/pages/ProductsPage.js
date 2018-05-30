import React, { Component } from 'react';
import TopBar from '../components/TopBar';
import Footer from '../components/Footer';

class ProductsPage extends Component {
  render() {
    return (
      <div>
        <TopBar />
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
                  <th>Name</th>
                  <th>Price</th>
                  <th>Stock</th>
                  <th />
                </tr>
              </thead>
              <tfoot>
                <tr>
                  <th style={{ colspan: '4' }}>
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
                  </th>
                </tr>
              </tfoot>
              <tbody>
                <tr>
                  <td />
                  <td />
                  <td />
                  <td className="is-link is-icon">
                    <a className="button is-success is-outlined">
                      <span>Edit</span>
                      <span className="icon is-small">
                        <i className="fa fa-pencil-square-o" />
                      </span>
                    </a>
                    <a className="button is-danger is-outlined">
                      <span>Delete</span>
                      <span className="icon is-small">
                        <i className="fa fa-times" />
                      </span>
                    </a>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </section>
        <Footer />
      </div>
    );
  }
}

export default ProductsPage;
