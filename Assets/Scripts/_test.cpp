#include <iostream>
#include <cmath> // Thay đổi từ <math.h> thành <cmath>

using namespace std;

// Hàm tính toán số vàng cần thiết
float requiredGold(int level, float a, float b)
{
    return (a * exp(b * level));
}

int main()
{
    // Khai báo biến
    float a = 10.0f; // Giá trị khởi điểm
    float b = 0.3f;  // Hệ số tăng trưởng
    int level = 10;  // Cấp độ

    // Nhập cấp độ từ người dùng (nếu cần)

    // Hiển thị kết quả
    for (int i = 0; i < level; i++)
    {
        cout << "So vang can thiet cho level " << i + 1 << " la: " << requiredGold(i, a, b) << endl;
    }

    return 0;
}
