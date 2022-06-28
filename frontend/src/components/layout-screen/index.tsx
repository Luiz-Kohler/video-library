import React from 'react';
import 'antd/dist/antd.css';
import './index.css';
import { UserOutlined, PlayCircleOutlined, AuditOutlined, GithubOutlined, MailOutlined, HomeOutlined, LinkedinOutlined } from '@ant-design/icons';
import { Col, Layout, Menu, Row } from 'antd';
import { useState } from 'react';
import type { MenuProps } from 'antd';
import Logo from '../../assets/logo'
import Title from 'antd/lib/typography/Title';
import { MenuClickEventHandler } from 'rc-menu/lib/interface';
import { useNavigate } from 'react-router-dom'

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
    getItem('Home', '1', <HomeOutlined />, () => navigate('/')),
    getItem('Clientes', '2', <UserOutlined />, () => navigate('/clientes')),
    getItem('Filmes', '3', <PlayCircleOutlined />, () => navigate('/filmes')),
    getItem('Locações', '4', <AuditOutlined />, () => navigate('/locacoes')),
  ];

  return (
    <Layout
      style={{
        minHeight: '100vh',
      }}
    >
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
            <Col style={{maxWidth: '1000px', minWidth: '100px'}}>
              {children}
            </Col>
          </Row>
        </Content>
        <Footer
          style={{
            textAlign: 'center',
          }}
        >
          <Title level={5}>Luiz Felipe dos Santos Kohler |  {<MailOutlined />} {<GithubOutlined />} {<LinkedinOutlined />}</Title>
        </Footer>
      </Layout>
    </Layout>
  );
};

export default App;