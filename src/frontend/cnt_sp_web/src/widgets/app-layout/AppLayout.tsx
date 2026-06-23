import { HomeOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { useState, type ReactNode } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import { AuthStubBanner } from '@/shared/auth';
import styles from './AppLayout.module.css';

const { Header, Content, Footer, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

function getItem(label: ReactNode, key: React.Key, icon?: React.ReactNode): MenuItem {
  return { key, icon, label } as MenuItem;
}

const items: MenuItem[] = [
  getItem(<Link to="/home">Главная</Link>, '/home', <HomeOutlined />),
];

/**
 * Оболочка приложения: меню, шапка, `Outlet` для страниц.
 */
export function AppLayout() {
  const [collapsed, setCollapsed] = useState(false);
  const { pathname } = useLocation();
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  return (
    <Layout className={styles.rootLayout}>
      <Sider
        collapsible
        collapsed={collapsed}
        onCollapse={setCollapsed}
        breakpoint="lg"
        collapsedWidth={0}
      >
        <div className={styles.logo}>SP</div>
        <Menu theme="dark" mode="inline" items={items} selectedKeys={[pathname]} />
      </Sider>
      <Layout className={styles.innerLayout}>
        <Header style={{ padding: 0, background: colorBgContainer }} />
        <Content
          style={{
            margin: '0 16px',
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
            flex: 1,
            display: 'flex',
            flexDirection: 'column',
            minHeight: 0,
          }}
        >
          <Breadcrumb style={{ margin: '16px 0' }} items={[{ title: 'Sample Project' }]} />
          <div className={styles.contentContainer}>
            <AuthStubBanner />
            <Outlet />
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          Sample Project ©{new Date().getFullYear()}
        </Footer>
      </Layout>
    </Layout>
  );
}
