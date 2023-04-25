import React from 'react';
import { Button, Layout, Toast } from '@douyinfe/semi-ui';
import Header from '@/components/layout/header';
import Footer from '@/components/layout/footer';
import Main from '@/components/layout/main';

function App() {
  return (
    <div className="app">
      <Layout>
        <Header />
        <Main />
        <Footer />
      </Layout>
    </div>
  );
}

export default App;
