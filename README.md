# Unity 游戏开发练习作品集

本项目包含三个独立的 Unity 游戏 Demo，分别展示 2D 和 3D 游戏开发中的不同技术点。

---

## 🎮 项目列表

| 项目 | 技术点 | 快速试玩 |
|:---|:---|:---|
| **2D 无尽跑酷** | 对象池 · 协程 · 物理跳跃 · UI状态机 | `BuildGame/EndlessRunner/无尽跑酷.exe` |
| **3D 打靶射击** | 射线检测 · 第一人称控制器 · 粒子特效 · 随机生成 | `BuildGame/TargetShooting/打靶射击.exe` |
| **3D 滚球收集** | 物理移动 · 触发器收集 · 摄像机平滑跟随 | `BuildGame/RollingBall/滚球收集.exe` |

> **运行要求**：Unity 2022.3 LTS 或更高版本（如需打开源码）

---

## 💡 核心实现亮点

### 2D 无尽跑酷
- **对象池**：管理障碍物的生成与回收，避免频繁 `Instantiate/Destroy`，减少 GC
- **协程**：实现障碍物无限动态生成（`yield return new WaitForSeconds`），替代 Update 累加计时
- **物理跳跃**：基于 `Rigidbody2D` 实现跳跃与落地检测，`OnCollisionEnter2D` 判断地面接触
- **难点攻克**：解决对象池复用后的状态重置问题，设计 `OnReset` 接口确保生成前状态初始化

### 3D 打靶射击
- **射线检测**：鼠标点击从屏幕中心发射射线，判断是否命中 `Target` 标签物体
- **第一人称控制**：WASD移动 + 鼠标视角旋转，`Mathf.Clamp` 限制上下 ±80° 防止视角翻转
- **随机生成**：每局在限定范围内随机位置生成 20 个靶子
- **粒子特效**：`ParticleSystem` 制作击中特效，设置 `Stop Action = Destroy` 实现自动清理
- **完整游戏循环**：开始 → 倒计时 → 得分统计 → 胜利/失败判断 → 重玩/退出

### 3D 滚球收集
- **物理移动**：`Rigidbody.AddForce` 实现球体移动，调整质量与阻力获得手感反馈
- **触发器收集**：金币的 `Collider` 设为 `Is Trigger`，`OnTriggerEnter` 实现收集逻辑
- **摄像机跟随**：`LateUpdate + Lerp` 实现平滑第三人称跟随，避免抖动
- **自转效果**：`Transform.Rotate` 实现金币原地旋转，不依赖 Animator 减少性能开销

---

## 📁 仓库结构

Unity-GameDemo/
├── GameDemo/ # Unity 项目源码
│ ├── EndlessRunner/ # 跑酷项目源码
│ ├── TargetShooting/ # 打靶项目源码
│ ├── RollingBall/ # 滚球项目源码
│ └── ...
├── BuildGame/ # 可执行文件（下载即玩）
│ ├── EndlessRunner/ # 跑酷 exe
│ ├── TargetShooting/ # 打靶 exe
│ └── RollingBall/ # 滚球 exe
└── README.md

---

## 🚀 如何快速试玩

### 方式一：下载 exe 文件（推荐）
1. 进入 `BuildGame/` 文件夹
2. 选择对应项目的文件夹
3. 点击 `.exe` 文件 → 点击右侧的 **Download** 按钮
4. 下载完成后，**双击运行**即可（无需安装 Unity）

### 方式二：用 Unity 打开源码
1. 用 Unity Hub 打开 `GameDemo/` 下的对应项目文件夹
2. 确保 Unity 版本 ≥ 2022.3 LTS
3. 打开 `Scenes` 文件夹中的主场景，点击运行

---
