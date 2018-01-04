using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる（追加 1）
    private Rigidbody myRigidbody;
    //前進するための力（追加 1）
    private float forwardForce = 800.0f;

    //左右に移動するための力（追加 2）
    private float turnForce = 500.0f;
    //左右の移動できる範囲（追加 2）
    private float movableRange = 3.4f;

    //ジャンプするための力（追加 3）
    private float upForce = 500.0f;

    //動きを減速させる計数（追加 4）
    private float coefficient = 0.95f;
    //ゲーム終了の判定（追加 4）
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト（追加 7）
    private GameObject stateText;

    //スコアを表示するテキスト（追加 8）
    private GameObject scoreText;

    //得点（追加 8）
    private int score = 0;

    //左ボタン押下の判定（追加 9）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加 9）
    private bool isRButtonDown = false;
    

    // Use this for initialization
    void Start()
    {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加 1）
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得（追加 7）
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得（追加 8）
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {

        //Unityちゃんに前方向の力を加える（追加 1）
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);


        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加 2,9）
        if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動（追加 2）
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
            
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右に移動（追加 2）
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }



        //Jumpステートの場合はJumpにfalseをセットする（追加 3）
        //（Jumpパラメータをtrueにしたままではジャンプアニメーションを何度も再生し続けてしまうので、
        //　ジャンプ状態の時にはif文の中でJumpパラメータがfalseになるようにする）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする（追加 3）
        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（追加 3）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加 3）
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

        //ゲーム終了ならUnityちゃんの動きを減速する（追加 4）
        if(this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

    
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合（追加 4）
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;

            //stateTextにGAME OVERを表示（追加 7）
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴール地点に到達した場合（追加 4）
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //stateTextにGAME CLEARを表示（追加 7）
            this.stateText.GetComponent<Text>().text = "CLEAR!!";

        }

        //コインに衝突した場合（追加 5）
        if (other.gameObject.tag == "CoinTag")
        {
            //スコアを加算（追加 8）
            this.score += 10;

            //ScoreTextに獲得した点数を表示（追加 8）
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生（追加 6）
            GetComponent<ParticleSystem>().Play();
            
            //衝突したコインのオブジェクトを破棄（追加 5）
            Destroy(other.gameObject);
        }

    }

    //ジャンプボタンを押した場合の処理（追加 9）
    public void GetMyJumpButtonDown()
    {
        //Jumpをしていない時の処理
        if (this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //左ボタンを押し続けた場合の処理（追加 9）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //左ボタンを話した場合の処理（追加 9）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理（追加 9）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //右ボタンを離した場合の処理（追加 9）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }


}