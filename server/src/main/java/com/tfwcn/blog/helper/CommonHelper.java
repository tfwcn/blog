package com.tfwcn.blog.helper;

import com.tfwcn.blog.dao.ErrorsMapper;
import com.tfwcn.blog.models.Errors;
import com.tfwcn.blog.models.api.ResponseInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.StringRedisTemplate;
import org.springframework.stereotype.Component;

import java.lang.reflect.Method;
import java.util.Date;
import java.util.UUID;

@Component
public class CommonHelper {
    private static StringRedisTemplate StringRedisTemplateObj;

    @Autowired
    public void setStringRedisTemplateObj(StringRedisTemplate tmpStringRedisTemplateObj) {
        StringRedisTemplateObj = tmpStringRedisTemplateObj;
    }

    private static ErrorsMapper errorDao;

    @Autowired
    public void setErrorDao(ErrorsMapper errorDao) {
        this.errorDao = errorDao;
    }

    /**
     * 获取异常详细信息
     * @param e 异常
     * @return 详细信息
     */
    public static String getExceptionDetail(Exception e) {
        StringBuffer stringBuffer = new StringBuffer(e.toString() + "\n");
        StackTraceElement[] messages = e.getStackTrace();
        int length = messages.length;
        for (int i = 0; i < length; i++) {
            stringBuffer.append("\t" + messages[i].toString() + "\n");
        }
        return stringBuffer.toString();
    }

    /**
     * 获取ID
     * @param obj model类对象
     */
    public static void getId(Object obj) {
        Class objType = obj.getClass();
        try {
            long id = 1;
            synchronized (CommonHelper.class) { //互斥锁
                //更新id最大值
                if (StringRedisTemplateObj.hasKey(objType.getTypeName()))
                    id = Long.parseLong(StringRedisTemplateObj.opsForValue().get(objType.getTypeName()));
                StringRedisTemplateObj.opsForValue().set(objType.getTypeName(), String.valueOf(id + 1));
            }
            Method setId = objType.getDeclaredMethod("setId", String.class);
            setId.invoke(obj, UUID.randomUUID().toString().replace("-", ""));
            Method setNum = objType.getDeclaredMethod("setNum", Long.class);
            setNum.invoke(obj, id);
            Date nowDate= new Date();
            Method setCreateTime = objType.getDeclaredMethod("setCreateTime", Date.class);
            setCreateTime.invoke(obj, nowDate);
            Method setUpdateTime = objType.getDeclaredMethod("setUpdateTime", Date.class);
            setUpdateTime.invoke(obj, nowDate);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * 记录错误日志
     * @param ex 错误
     */
    public static ResponseInfo SaveErrorLog(Exception ex){
        //记录错误
        Errors errorInfo = new Errors();
        CommonHelper.getId(errorInfo);
        errorInfo.setMessage(ex.getMessage());
        errorInfo.setDetail(CommonHelper.getExceptionDetail(ex));
        errorDao.insert(errorInfo);
        //返回值
        ResponseInfo responseInfo = new ResponseInfo(500, "错误代码：" + errorInfo.getNum());
        return responseInfo;
    }
}
