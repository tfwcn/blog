package com.tfwcn.blog.helper;

import org.apache.tomcat.util.codec.binary.Base64;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.Random;

public class ImageHelper {
    private static Random random = new Random();

    /**
     * 图片转Base64
     * @param img 图片
     * @return Base64字符串
     * @throws IOException 异常
     */
    public static String GetBase64(BufferedImage img) throws IOException {
        return new String(Base64.encodeBase64(GetImageBytes(img)));
    }

    /**
     * 获取图片byte数组,png格式
     * @param img 图片
     * @return byte数组
     * @throws IOException 异常
     */
    public static byte[] GetImageBytes(BufferedImage img) throws IOException {
        ByteArrayOutputStream os = new ByteArrayOutputStream();//新建流
        ImageIO.write(img, "png", os);
        return os.toByteArray();
    }

    /**
     * 创建验证码图片
     * @param number 4位数字
     * @return 图片
     * @throws IOException 异常
     * @throws FontFormatException 异常
     */
    public static BufferedImage CreateVerificationCode(int number) throws IOException, FontFormatException {
        int fontWidth = 100, fontHeight = 38;
        BufferedImage img = new BufferedImage(fontWidth, fontHeight, BufferedImage.TYPE_INT_RGB);
        Graphics2D graphics = (Graphics2D) img.getGraphics();
        Color bgColor = GetRandomColor();
        Color fontColor = GetInvertColor(bgColor);
        graphics.setColor(bgColor);
        graphics.fillRect(0, 0, fontWidth, fontHeight);
//        graphics.setBackground(bgColor);
        Font numFont = Font.createFont(Font.PLAIN, new File("./src/main/resources/AGENTRED.TTF")).deriveFont(Font.PLAIN, 20);
        graphics.setFont(numFont);
//        graphics.setColor(fontColor);
        graphics.setPaint(fontColor);
        int numX = random.nextInt(10);
        for (int i = 0; i < 4; i++) {
            graphics.drawString(String.format("%d", number / (i == 0 ? 1 : 10 * i) % 10), numX, random.nextInt(10) + 20);
            numX += random.nextInt(5) + 15;
        }
        graphics.dispose();
        return img;
    }

    /**
     * 获取0-bound（不包含bound）的随机整数
     * @param bound 最大值
     * @return 整数
     */
    public static int GetRandomInt(int bound) {
        return random.nextInt(bound);
    }

    /**
     * 获取随机颜色
     * @return 颜色
     */
    public static Color GetRandomColor() {
        float h = random.nextFloat();
        float s = random.nextFloat();
        float b = random.nextFloat();
        return Color.getHSBColor(h, s, b);
    }

    /**
     * 产生对应指定颜色差异较大的颜色
     * @param color 颜色
     * @return 差异颜色
     */
    public static Color GetInvertColor(Color color) {
        int red = color.getRed();
        int green = color.getGreen();
        int blue = color.getBlue();
        float[] hsb = Color.RGBtoHSB(red, green, blue, null);
        float h = hsb[0];
        float s = hsb[1];
        float b = hsb[2] > 0.5f ? 0.2f : 0.8f;
        return Color.getHSBColor(h, s, b);
    }
}
