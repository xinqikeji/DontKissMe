using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// 场景加载管理器，负责关卡切换、武器设置和广告播放等功能
    /// </summary>
    internal class SceneLoader : MonoBehaviour
    {
        // 武器对象数组
        [SerializeField] private GunParent[] _weapons;
        // 滑块精灵数组
        [SerializeField] private Sprite[] _sliders;
        // 非激活状态图像数组
        [SerializeField] private Sprite[] _noActiveImages;
        // 物品进度条组件
        [SerializeField] private ItemProgress _itemProgress;
        // 字节游戏广告管理器
        public ByteGameAdManager byteGameAdManager;

        // 每10个关卡解锁一个新武器
        private int _countLevelToNextWeapon = 10;
        // 当前关卡编号
        private int _currentLevel;
        // 当前武器编号
        private int _numberWeapon;
        // 关卡保存标志位
        private bool _isLevelSave;
        // 游戏开始时间
        public float StartTime { get; private set; }

        /// <summary>
        /// 初始化场景加载器，在Awake阶段完成关卡数据初始化和场景检查
        /// </summary>
        private void Awake()
        {
            // 增加总关卡计数
            PlayerPrefs.SetInt(PlayerPrefsConst.LevelCount, PlayerPrefs.GetInt(PlayerPrefsConst.LevelCount) + 1);
            // 获取当前关卡编号
            _currentLevel = PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel);
            // 上报关卡开始事件
            IntegrationMetric.Instance.OnLevelStart(_currentLevel,
                                                    _currentLevel + "_DKM",
                                                    PlayerPrefs.GetInt(PlayerPrefsConst.LevelCount),
                                                    (PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel) - 1) / _countLevelToNextWeapon,
                                                    false);

            // 检查是否有保存的场景索引，如果有且与当前场景不同则加载保存的场景
            if (PlayerPrefs.HasKey(PlayerPrefsConst.CurrentLevel))
            {
                int sceneIndex = PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel);
                if (sceneIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    SceneManager.LoadScene(sceneIndex);
                }
            }
        }

        /// <summary>
        /// 在Start阶段设置下一关卡使用的武器并记录开始时间
        /// </summary>
        private void Start()
        {
            SetNextWeapon();
            StartTime = Time.time;
        }

        /// <summary>
        /// 进入下一个场景，先播放激励广告
        /// </summary>
        public void NextScene()
        {
            // 播放激励广告
            byteGameAdManager.PlayRewardedAd("3bmfb48h31fn227wfe",
                        (isValid, duration) =>
                        {
                            // isValid表示广告是否完整播放完毕
                            Debug.LogError(0);
                            if (isValid)
                            {
                                // 广告播放完成后保存关卡进度并加载下一关
                                int nextSceneIndex = SaveLevel();
                                Debug.Log(PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel));
                                LoadScene(nextSceneIndex);
                            }
                        },
                        // 广告播放出错回调
                        (errCode, errMsg) =>
                        {
                            Debug.LogError(1);
                        });
        }

        /// <summary>
        /// 返回主菜单界面
        /// </summary>
        public void BackMian()
        {
            // 保存当前关卡进度
            int nextSceneIndex = SaveLevel();
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel));
            // 直接加载主菜单场景(索引为0)
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// 保存当前关卡进度并计算下一个场景索引
        /// </summary>
        /// <returns>下一个场景的索引</returns>
        public int SaveLevel()
        {
            // 如果已经保存过则直接返回已保存的场景索引
            if (_isLevelSave)
                return PlayerPrefs.GetInt(PlayerPrefsConst.CurrentLevel);
            _isLevelSave = true;

            // 获取当前关卡编号
            _currentLevel = PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel);
            // 计算下一个场景索引
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // 如果已经是最后一个场景，则循环回到第一个关卡场景
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 1;
            }

            // 增加当前关卡编号
            ++_currentLevel;

            // 保存场景索引和关卡编号
            PlayerPrefs.SetInt(PlayerPrefsConst.CurrentLevel, nextSceneIndex);
            PlayerPrefs.SetInt(PlayerPrefsConst.NumberLevel, _currentLevel);
            return nextSceneIndex;
        }

        /// <summary>
        /// 重新开始当前关卡
        /// </summary>
        public void Restart()
        {
            // 加载当前场景
            LoadScene(SceneManager.GetActiveScene().buildIndex);
            // 恢复正常时间流速
            Time.timeScale = 1;

            // 注释掉的激励广告代码，可用于后续扩展
            //byteGameAdManager.PlayRewardedAd("3bmfb48h31fn227wfe",
            //            (isValid, duration) =>
            //            {
            //                //isValid广告是否播放完，正常游戏逻辑在以下部分
            //                Debug.LogError(0);
            //                if (!isValid)
            //                {
            //                }
            //            },
            //               (errCode, errMsg) =>
            //               {
            //                   Debug.LogError(1);
            //               });
        }

        /// <summary>
        /// 加载指定索引的场景
        /// </summary>
        /// <param name="indexScene">场景索引</param>
        private void LoadScene(int indexScene) => SceneManager.LoadScene(indexScene);

        /// <summary>
        /// 设置下一关卡使用的武器
        /// </summary>
        private void SetNextWeapon()
        {
            // 计算当前应该使用的武器编号(每_countLevelToNextWeapon个关卡切换一次武器)
            _numberWeapon = ((PlayerPrefs.GetInt(PlayerPrefsConst.NumberLevel)) - 1) / _countLevelToNextWeapon;
            PlayerPrefs.SetInt(PlayerPrefsConst.LevelLoop, _numberWeapon);

            // 如果武器编号发生变化，清理物品进度并更新武器编号
            if (_numberWeapon != PlayerPrefs.GetInt(PlayerPrefsConst.IndexWeapon))
            {
                _itemProgress.Clear();
                PlayerPrefs.SetInt(PlayerPrefsConst.IndexWeapon, _numberWeapon);
            }

            // 将所有武器设为非激活状态
            foreach (var weapon in _weapons)
            {
                weapon.gameObject.SetActive(false);
            }

            // 激活对应编号的武器
            if (_numberWeapon < _weapons.Length)
            {
                // 设置滑块样式
                if (_sliders.Length > _numberWeapon)
                    _itemProgress.SetSlider(_sliders[_numberWeapon], _noActiveImages[_numberWeapon]);
                _weapons[_numberWeapon].gameObject.SetActive(true);
                return;
            }

            // 如果武器编号超出范围，则默认激活最后一个武器
            _weapons[_weapons.Length - 1].gameObject.SetActive(true);
        }
    }
}