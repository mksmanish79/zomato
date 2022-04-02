import { useState } from "react";

const Login = function () {
    const [isSignIn, setisSignIn] = useState(true);
    return (
        <div className="container my-5">
            <div className="row">
                <div className="col-md-4 offset-md-4">
                    <form>
                        <div className="form-group">
                            <label for="exampleInputEmail1">Email address</label>
                            <input type="email" className="form-control" id="txtloginemail" aria-describedby="emailHelp" placeholder="Enter email" />
                        </div>
                        <div className="form-group">
                            <label for="exampleInputPassword1">Password</label>
                            <input type="password" className="form-control" id="txtloginpassword" placeholder="Password" />
                        </div>
                        <div className="form-group text-center">
                            {isSignIn ? <button type="button" className="btn btn-primary mt-1">Sign In</button> : <button type="button" className="btn btn-primary mt-1">Sign Up</button>}
                            <hr />
                            {isSignIn ? <button type="button" className="btn mt-1" onClick={()=>setisSignIn(false)}>Sign Up</button> : <button type="button" className="btn mt-1" onClick={()=>setisSignIn(true)}>Sign In</button>}
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default Login;