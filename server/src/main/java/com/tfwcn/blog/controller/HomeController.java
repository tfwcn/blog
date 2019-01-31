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
@RequestMapping("/")
public class HomeController {

    /**
     * 构造函数
     */
    @Autowired
    public HomeController() {

    }

    /**
     * 主页 /
     *
     * @param reqMap loginName 登录账号
     * @return 返回状态码，1：成功
     */
    @RequestMapping(method = RequestMethod.GET, path = "")
    public ResponseEntity<?> Index() {
        return ResponseEntity.ok("API server is running.");
    }
}
