import React from 'react';
import 'antd/dist/antd.css';
import './index.css';
import { UserOutlined, PlayCircleOutlined, AuditOutlined, GithubOutlined, MailOutlined, LinkedinOutlined } from '@ant-design/icons';
import { Col, Layout, Menu, Row, Tooltip } from 'antd';
import { useState } from 'react';
import type { MenuProps } from 'antd';
import Logo from '../../assets/logo'
import Title from 'antd/lib/typography/Title';
import { MenuClickEventHandler } from 'rc-menu/lib/interface';
import { useNavigate } from 'react-router-dom'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const { Content, Footer, Sider } = Layout;
type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  onClick?: MenuClickEventHandler,
  children?: MenuItem[],
): MenuItem {
  return {
    key,
    icon,
    onClick,
    children,
    label,
  } as MenuItem;
}

const App = ({ children }: any) => {
  const navigate = useNavigate();
  const [collapsed, setCollapsed] = useState(false);

  const items: MenuItem[] = [
    getItem('Locações', '1', <AuditOutlined />, () => navigate('/locacoes')),
    getItem('Clientes', '2', <UserOutlined />, () => navigate('/clientes')),
    getItem('Filmes', '3', <PlayCircleOutlined />, () => navigate('/filmes')),
  ];

  return (
    <Layout
      style={{
        minHeight: '100vh',
      }}
    >
      <ToastContainer />
      <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
        <div className="logo" >
          <Logo />
        </div>
        <Menu theme="dark" mode="inline" items={items} />
      </Sider>
      <Layout className="site-layout">
        <Content
          style={{
            margin: '0 16px',
          }}
        >
          <Row justify='center' className='flexbox'>
            <Col style={{ maxWidth: '1000px', minWidth: '100px' }}>
              {children}
            </Col>
          </Row>
        </Content>
        <Footer
          style={{
            textAlign: 'center',
          }}
        >
          <Title level={5}>Luiz Felipe dos Santos Kohler | 
              <Tooltip title="luizfelipekohler03@gmail.com">
                <MailOutlined style={{margin: '2px'}}/>
              </Tooltip>
              <Tooltip title="Github">
                <GithubOutlined onClick={() => window.open("https://github.com/Luiz-Kohler")} style={{margin: '2px'}}/>
              </Tooltip>
              <Tooltip title="Linkedin">
                <LinkedinOutlined onClick={() => window.open("https://www.linkedin.com/in/luiz-kohler-50b6a1191/")} style={{margin: '2px'}}/>
              </Tooltip>
          </Title>
        </Footer>
      </Layout>
    </Layout>
  );
};

export default App;