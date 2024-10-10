 

public abstract class BaseState 
{
    //调用EnemyController脚本，命名为currentEnemy
    protected EnemyController currentEnemy;

    //想要在哪个状态调用EnemyController就可以在对应的括号内传入，并命名
    public abstract void OnEnter(EnemyController enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();


}
