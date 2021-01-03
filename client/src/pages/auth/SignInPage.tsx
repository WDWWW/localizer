import React, { MouseEventHandler } from 'react'
import { Button, Checkbox, Divider, Form, Input, notification } from 'antd'
import { GithubOutlined, LockOutlined, UserOutlined } from '@ant-design/icons'
import { Link, Redirect, RouteProps, useHistory } from 'react-router-dom'
import styled from 'styled-components'
import { parse } from 'qs'
import { useStores } from '../../commom/stores/StoreProvider'

const Header = styled.div`
	margin-bottom: 48px;
	text-align: center;

	h1 {
		font-weight: 400;
		margin-bottom: 12px;
	}
`

const ForgotPassword = styled.a`
	float: right;
`

type SignInState = {
	email: string
	password: string
	remember: boolean
}

const SignInPage: React.FC<RouteProps> = props => {
	const { redirect } = parse(props.location?.search ?? '', { ignoreQueryPrefix: true })
	const { authStore } = useStores()
	const { push } = useHistory()

	if (authStore.isLoggedIn) return <Redirect to="/" />

	const onSubmit = async (data: SignInState) => {
		const { email, password } = data
		if (await authStore.signIn(email, password)) {
			push((redirect as string) ?? '/')
		} else {
			notification.error({
				message: 'Fail to login',
				description: 'email or password is incorrect.',
			})
		}
	}

	const notYetImpl: MouseEventHandler<HTMLElement> = e => {
		e.preventDefault()
		notification.warn({
			message: 'not yet implemented the feature.',
			placement: 'topRight',
		})
	}

	return (
		<>
			<Header>
				<h1>Localizer</h1>
				<p>Feature rich self hosted localization system.</p>
			</Header>
			<Form name="normal_sign-in" className="sign-in-form" onFinish={onSubmit}>
				<Form.Item name="email" rules={[{ required: true, message: 'Please input your Username!' }]}>
					<Input prefix={<UserOutlined />} placeholder="Username" />
				</Form.Item>
				<Form.Item name="password" rules={[{ required: true, message: 'Please input your Password!' }]}>
					<Input prefix={<LockOutlined />} type="password" placeholder="Password" />
				</Form.Item>
				<Form.Item>
					<Form.Item name="remember" valuePropName="checked" noStyle>
						<Checkbox>Remember me</Checkbox>
					</Form.Item>

					<ForgotPassword href="" onClick={notYetImpl}>
						Forgot password
					</ForgotPassword>
				</Form.Item>
				<Form.Item style={{ marginBottom: '16px' }}>
					<Button type="primary" htmlType="submit" block>
						Sign In
					</Button>
				</Form.Item>
				<Form.Item>
					<Link to="/auth/sign-up">
						<Button type="ghost" htmlType="button" block>
							Register Account
						</Button>
					</Link>
				</Form.Item>
				<Divider plain>Or</Divider>
				<Form.Item noStyle>
					<Button type="ghost" htmlType="button" block>
						<GithubOutlined />
						Github
					</Button>
				</Form.Item>
			</Form>
		</>
	)
}

export default SignInPage
