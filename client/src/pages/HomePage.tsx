import React from 'react'
import { Redirect, Route, Switch, useHistory } from 'react-router-dom'
import ProjectsPage from './ProjectsPage'
import { Content, Header } from 'antd/es/layout/layout'
import { Avatar, Dropdown, Layout, Menu } from 'antd'
import { CaretDownOutlined } from '@ant-design/icons'
import styled from 'styled-components'
import { useStores } from '../commom/stores/StoreProvider'

const AccountButton = styled.div`
	float: right;
	padding: 0 10px;
	cursor: pointer;
`

const HomePage: React.FC = () => {
	const { authStore } = useStores()

	const onSignOut = () => authStore.signOut()

	return (
		<>
			<Layout style={{ height: '100vh' }}>
				<Header>
					<div style={{ float: 'left' }}>
						<h1 style={{ color: 'white', padding: '0 20px', background: 'rgba(0,0,0,0.1)' }}>Localizer</h1>
					</div>
					<Menu theme="dark" mode="horizontal" defaultSelectedKeys={['2']} style={{ float: 'left' }}>
						<Menu.Item key="1">projects</Menu.Item>
						<Menu.Item key="2">todos</Menu.Item>
					</Menu>
					<Dropdown
						overlay={
							<Menu style={{ padding: '10px' }}>
								<Menu.Item>Account settings</Menu.Item>
								<Menu.Item disabled>System settings</Menu.Item>
								<Menu.Divider />
								<Menu.Item danger onClick={onSignOut}>
									Sing out
								</Menu.Item>
							</Menu>
						}
						placement="bottomRight"
					>
						<AccountButton>
							<Avatar style={{ backgroundColor: '#7265e6', marginRight: '4px' }} size="large">
								name
							</Avatar>
							<CaretDownOutlined style={{ color: 'white' }} />
						</AccountButton>
					</Dropdown>
				</Header>
				<Content>
					<Switch>
						<Route exact path="/">
							<Redirect to="/projects" />
						</Route>
						<Route path="/projects" component={ProjectsPage} />
						<Route>
							<Redirect to="/not-found" />
						</Route>
					</Switch>
				</Content>
			</Layout>
		</>
	)
}
export default HomePage
