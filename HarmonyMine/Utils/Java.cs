using IKVM.Attributes;
using java.util.function;
using net.minecraft.entity.ai.brain.task;
using net.minecraft.server.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Utils;

public class PredicateLambda<T> : Predicate {
    Func<T, bool> _predicate;

    public PredicateLambda(Func<T, bool> predicate) {
        _predicate = predicate;
    }

    [HideFromJava]
    public bool test(object t) {
        return _predicate.Invoke((T)t);
    }

    public Predicate and(Predicate A_1) {
        return new PredicateLambda<T>(t => this.test(t) && A_1.test(t));
    }

    public Predicate negate() {
        return new PredicateLambda<T>(t => !this.test(t));
    }

    public Predicate or(Predicate A_1) {
        return new PredicateLambda<T>(t => this.test(t) || A_1.test(t));
    }
    public static implicit operator PredicateLambda<T>(Func<T, bool> predicate) => new PredicateLambda<T>(predicate);
}

//public class JavaRunnable : Runnable {
//    Action action;

//    public JavaRunnable(Action action) {
//        this.action = action;
//    }

//    [HideFromJava]
//    public void run() => action.Invoke();
//    public static implicit operator JavaRunnable(Action action) => new JavaRunnable(action);
//}