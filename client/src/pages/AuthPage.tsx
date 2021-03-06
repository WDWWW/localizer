import React from 'react'
import { Redirect, Route, Switch, useRouteMatch } from 'react-router-dom'
import SignInPage from './auth/SignInPage'
import SignUpPage from './auth/SignUpPage'
import styled from 'styled-components'
import { useStores } from '../commom/stores/StoreProvider'
import { observer } from 'mobx-react-lite'
import ResetPasswordPage from './auth/ResetPasswordPage'

const AuthPageBackground = styled.div`
	display: flex;
	justify-content: center;
	align-items: center;
	height: 100vh;
	position: relative;

	&::before {
		content: '';
		position: absolute;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		background: url('https://images.unsplash.com/photo-1607684548943-cc0e125fc8f7?ixlib=rb-1.2.1&q=80&fm=jpg&crop=entropy&cs=tinysrgb&w=1080&fit=max')
			no-repeat center;
		background-size: cover;
		filter: brightness(50%);
	}
`

const AuthPageWrapper = styled.div`
	position: relative;
	background: white;
	min-width: 400px;
	padding: 48px;
	border: 1px solid #eee;
	border-radius: 4px;
	box-shadow: rgba(149, 157, 165, 0.2) 0 8px 24px;
	transition: all 0.4s ease-out;
`

const AuthPage: React.FC = observer(() => {
	const { path } = useRouteMatch()

	const { authStore } = useStores()

	if (authStore.isLoggedIn) return <Redirect to="/" />

	return (
		<AuthPageBackground>
			<AuthPageWrapper>
				<Switch>
					<Route exact path={path}>
						<Redirect to={`${path}/sign-in`} />
					</Route>
					<Route path="/auth/sign-in" component={SignInPage} />
					<Route path="/auth/sign-up" component={SignUpPage} />
					<Route path="/auth/reset-password" component={ResetPasswordPage} />
				</Switch>
			</AuthPageWrapper>
		</AuthPageBackground>
	)
})
export default AuthPage
