import { useState } from "react";
import { get, isEmpty } from "lodash";

const Login = function () {
    const [signinform, setsigninform] = useState({ email: '', password: '' });
    const [isSignIn, setisSignIn] = useState(true);
    const [error, seterror] = useState({});

    const validateForm = () => {
        const _error = {};
        if (!get(signinform, "email")) {
            _error["email"] = "Email is required!";
        }
        if (!get(signinform, "password")) {
            _error["password"] = "Password is required!";
        }
        seterror(_error);
        if (isEmpty(_error))
            return true;
        else
            return false;
    };

    const doSignIn = () => {
        if (validateForm()) {

        }
    };
    const doOnchange = (e, name) => {
        const _value = get(e, 'target.value');
        setsigninform(s => {
            return { ...s, [name]: _value };
        });
    };

    return (
        <div className="container my-5">
            <div className="row">
                <div className="col-md-4 offset-md-4">
                    <form>
                        <div className="form-group">
                            <label htmlFor="exampleInputEmail1">Email address</label>
                            <input type="email" className="form-control" id="txtloginemail" aria-describedby="emailHelp" placeholder="Enter email" value={signinform.email} onChange={e => doOnchange(e, 'email')} />
                            {!isEmpty(get(error, "email")) ? <span style={{ color: 'red' }}>{get(error, "email")}</span> : ''}
                        </div>
                        <div className="form-group">
                            <label htmlFor="exampleInputPassword1">Password</label>
                            <input type="password" className="form-control" id="txtloginpassword" placeholder="Password" value={signinform.password} onChange={e => doOnchange(e, 'password')} />
                            {get(error, 'password') && <span className="error">{get(error, 'password')}</span>}
                        </div>
                        <div className="form-group text-center">
                            {isSignIn ? <button type="button" className="btn btn-primary mt-1" onClick={doSignIn}>Sign In</button> : <button type="button" className="btn btn-primary mt-1">Sign Up</button>}
                            <hr />
                            {isSignIn ? <button type="button" className="btn mt-1" onClick={() => setisSignIn(false)}>Sign Up</button> : <button type="button" className="btn mt-1" onClick={() => setisSignIn(true)}>Sign In</button>}
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default Login;