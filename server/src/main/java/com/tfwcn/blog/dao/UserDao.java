package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.UserInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

@Transactional
public interface UserDao extends CrudRepository<UserInfo, String> {
    public UserInfo findByNum(long num);
    public UserInfo findByLoginName(String loginName);
}
