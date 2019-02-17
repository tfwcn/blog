package com.tfwcn.blog.controller;

import com.tfwcn.blog.dao.UsersMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.helper.ImageHelper;
import com.tfwcn.blog.models.Users;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.ResourceLoader;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.util.HashMap;
import java.util.Map;

/**
 * 用户操作类
 */
@RestController
@RequestMapping("/api/user")
public class UserController {
    @Autowired
    private UsersMapper userDao;

    private final ResourceLoader resourceLoader;

    /**
     * 构造函数
     *
     * @param resourceLoader 用于获取上传文件
     */
    @Autowired
    public UserController(ResourceLoader resourceLoader) {
        this.resourceLoader = resourceLoader;
    }

    /**
     * 检测登录账号是否存在 /api/user/checkUser
     *
     * @param reqMap loginName 登录账号
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/check_user")
    public ResponseEntity<?> checkUser(@RequestBody Map<String, Object> reqMap) {
        try {
            //获取参数
            String loginName = (String) reqMap.get("loginName");
            Users tmpUserInfo = userDao.selectByLoginName(loginName);
            if (tmpUserInfo == null) {
                //返回值，用户名不存在
                ResponseInfo responseInfo = new ResponseInfo(2, loginName + "用户名不存在！");
                return ResponseEntity.ok(responseInfo);
            }
            //返回值，成功
            ResponseInfo responseInfo = new ResponseInfo(0, null);
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 用户注册 /api/user/register
     *
     * @param reqMap loginName,password,userName,verificationCode
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/register")
    public ResponseEntity<?> register(HttpServletRequest request, HttpServletResponse response,
                                      @RequestBody Map<String, Object> reqMap) {
        try {
            //获取参数
            String loginName = (String) reqMap.get("loginName");
            String password = (String) reqMap.get("password");
            String userName = (String) reqMap.get("userName");
            String verificationCode = (String) reqMap.get("verificationCode");
            //获取验证码
            HttpSession session = request.getSession();
            Object vCode = session.getAttribute("verificationCode");
            if (vCode == null || !String.format("%04d", vCode).equals(verificationCode)) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(2, "验证码有误！");
                return ResponseEntity.ok(responseInfo);
            } else if (loginName == null || loginName.equals("")) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(3, "登录账号不能为空！");
                return ResponseEntity.ok(responseInfo);
            } else if (password == null || password.equals("")) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(4, "密码不能为空！");
                return ResponseEntity.ok(responseInfo);
            } else if (userName == null || userName.equals("")) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(5, "用户名不能为空！");
                return ResponseEntity.ok(responseInfo);
            }
            Users tmpUserInfo = userDao.selectByLoginName(loginName);
            if (tmpUserInfo != null) {
                //返回值，登录账号已存在
                ResponseInfo responseInfo = new ResponseInfo(6, loginName + "登录账号已存在！");
                return ResponseEntity.ok(responseInfo);
            }
            //插入数据
            Users userInfo = new Users();
            CommonHelper.getId(userInfo);
            userInfo.setLoginName(loginName);
            userInfo.setPassword(password);
            userInfo.setUserName(userName);
            userDao.insert(userInfo);
            //返回值，成功
            ResponseInfo responseInfo = new ResponseInfo(0, "注册成功！");
            return ResponseEntity.ok(responseInfo);
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 用户登录 /api/user/login
     *
     * @param request  请求 提交时，无需传参
     * @param response 返回结果 提交时，无需传参
     * @param reqMap   loginName,password,verificationCode
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.POST, path = "/login")
    public ResponseEntity<?> login(HttpServletRequest request, HttpServletResponse response,
                                   @RequestBody Map<String, Object> reqMap) {
        try {
            //获取参数
            String loginName = (String) reqMap.get("loginName");
            String password = (String) reqMap.get("password");
            String verificationCode = (String) reqMap.get("verificationCode");
            //获取用户数据
            Users tmpUserInfo = userDao.selectByLoginName(loginName);
            //获取验证码
            HttpSession session = request.getSession();
            Object vCode = session.getAttribute("verificationCode");
            if (vCode == null || !String.format("%04d", vCode).equals(verificationCode)) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(2, "验证码有误！");
                return ResponseEntity.ok(responseInfo);
            } else if (tmpUserInfo == null || !tmpUserInfo.getPassword().equals(password)) {
                //返回值，失败
                ResponseInfo responseInfo = new ResponseInfo(3, loginName + "用户名或密码有误！");
                return ResponseEntity.ok(responseInfo);
            } else {
                //返回值，成功
                ResponseInfo responseInfo = new ResponseInfo(0, loginName + "登录成功！");
                return ResponseEntity.ok(responseInfo);
            }
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }

    /**
     * 获取验证码图片 /api/user/verification_code
     *
     * @param request  请求 提交时，无需传参
     * @param response 返回结果 提交时，无需传参
     * @return 返回Json，1：成功
     */
    @RequestMapping(method = RequestMethod.GET, path = "/verification_code")
    public ResponseEntity<?> VerificationCode(HttpServletRequest request, HttpServletResponse response) {
        try {
            int randonNum = ImageHelper.GetRandomInt(10000);
            HttpSession session = request.getSession();
            session.setAttribute("verificationCode", randonNum);
            HashMap<String, String> data = new HashMap<String, String>();
            data.put("img", "data:image/png;base64," + ImageHelper.GetBase64(ImageHelper.CreateVerificationCode(randonNum)));
            //返回值，成功
            ResponseInfo responseInfo = new ResponseInfo(1, data);
            return ResponseEntity.ok(responseInfo);
//            response.setContentType("image/png");
//
//            OutputStream stream = response.getOutputStream();
//            stream.write(ImageHelper.GetImageBytes(ImageHelper.CreateVerificationCode(randonNum)));
//            stream.flush();
//            stream.close();
        } catch (Exception ex) {
            //返回值
            ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
            return ResponseEntity.ok(responseInfo);
        }
    }
}
