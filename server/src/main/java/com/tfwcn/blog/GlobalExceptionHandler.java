package com.tfwcn.blog;

import com.tfwcn.blog.dao.ErrorsMapper;
import com.tfwcn.blog.helper.CommonHelper;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;

/**
 * 全局异常处理
 */
@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(value = Exception.class)
    @ResponseBody
    public ResponseEntity<?> jsonErrorHandler(HttpServletRequest req, Exception ex) {
        System.out.println(ex);
        //返回值
        ResponseInfo responseInfo = CommonHelper.SaveErrorLog(ex);
        return ResponseEntity.ok(responseInfo);
    }
}
