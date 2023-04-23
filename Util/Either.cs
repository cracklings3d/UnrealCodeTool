namespace UCT.Util;

public class Either<TLeft, TRight> {
  private readonly TLeft  left;
  private readonly TRight right;
  private readonly bool   isLeft;

  private Either(TLeft left) {
    this.left   = left;
    this.isLeft = true;
  }

  private Either(TRight right) {
    this.right  = right;
    this.isLeft = false;
  }

  public static Either<TLeft, TRight> Left(TLeft value) {
    return new Either<TLeft, TRight>(value);
  }

  public static Either<TLeft, TRight> Right(TRight value) {
    return new Either<TLeft, TRight>(value);
  }

  public T Match<T>(Func<TLeft, T> leftFunc, Func<TRight, T> rightFunc) {
    return this.isLeft ? leftFunc(this.left) : rightFunc(this.right);
  }

  public bool IsLeft  => isLeft;
  public bool IsRight => !isLeft;
  public TLeft LeftValue => isLeft
                                ? left
                                : throw new InvalidOperationException(
                                      "Cannot access Left when Either is Right"
                                  );
  public TRight RightValue => isLeft
                                  ? throw new InvalidOperationException(
                                        "Cannot access Right when Either is Left"
                                    )
                                  : right;

  public static implicit operator Either<TLeft, TRight>(TLeft left) {
    return Left(left);
  }

  public static implicit operator Either<TLeft, TRight>(TRight right) {
    return Right(right);
  }
}
