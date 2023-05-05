import { Layout } from '@douyinfe/semi-ui';
import Header from '@/components/layout/header';
import Footer from '@/components/layout/footer';
import BackTop from '@/components/layout/backTop';
import Main from '@/components/layout/main';
import './App.scss';

function App() {
  return (
    <div className="app">
      <Layout>
        <Header />
        <Main />
        <Footer />
        <BackTop />
      </Layout>
    </div>
  );
}

export default App;
