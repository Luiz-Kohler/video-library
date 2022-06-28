import React from 'react';

import Routes from './routes';
import { BrowserRouter } from 'react-router-dom'

import Layout from './components/layout-screen'

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes />
      </Layout>
    </BrowserRouter>
  );
}

export default App;
