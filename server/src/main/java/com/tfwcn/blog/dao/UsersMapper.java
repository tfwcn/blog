package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Users;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface UsersMapper {
    int deleteByPrimaryKey(String id);

    int insert(Users record);

    Users selectByPrimaryKey(String id);

    Users selectByLoginName(String loginName);

    List<Users> selectAll();

    int updateByPrimaryKey(Users record);
}