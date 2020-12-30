import React, { MouseEventHandler } from 'react'
import { Button, Checkbox, Divider, Form, Input, notification } from 'antd'
import { GithubOutlined, LockOutlined, UserOutlined } from '@ant-design/icons'
import { Link } from 'react-router-dom'
import styled from 'styled-components'

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

const SignInPage: React.FC = () => {
	const onFinish = (values: any) => {
		console.log('Received values of form: ', values)
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
			<Form name="normal_sign-in" className="sign-in-form">
				<Form.Item name="username" rules={[{ required: true, message: 'Please input your Username!' }]}>
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
