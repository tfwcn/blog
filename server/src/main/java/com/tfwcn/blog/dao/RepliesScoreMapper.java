package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.RepliesScore;
import java.util.List;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface RepliesScoreMapper {
    int deleteByPrimaryKey(String id);

    int insert(RepliesScore record);

    RepliesScore selectByPrimaryKey(String id);

    List<RepliesScore> selectAll();

    int updateByPrimaryKey(RepliesScore record);
}