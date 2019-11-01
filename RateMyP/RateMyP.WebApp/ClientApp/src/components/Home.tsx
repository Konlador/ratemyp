import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Tipo kažkoks faina home page, kur per viduri yra searchas.</h1>
  </div>
);

export default connect()(Home);
