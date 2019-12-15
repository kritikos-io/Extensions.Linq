namespace Kritikos.Extensions.Linq
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	[ExcludeFromCodeCoverage]
	public static class ExpressionDeconstructor
	{
		public static void Deconstruct(
			this BinaryExpression expr,
			out ExpressionType operation,
			out Expression left,
			out Expression right)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			left = expr.Left;
			right = expr.Right;
		}

		public static void Deconstruct(
			this BlockExpression expr,
			out ExpressionType operation,
			out List<Expression> expressions,
			out Expression result,
			out List<ParameterExpression> parameters)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			result = expr.Result;
			expressions = expr.Expressions.ToList();
			parameters = expr.Variables.ToList();
		}

		public static void Deconstruct(
			this CatchBlock expr,
			out Expression body,
			out Expression filter,
			out Type exceptionType,
			out ParameterExpression exception)
		{
			body = expr?.Body
					?? throw new ArgumentNullException(nameof(expr));
			filter = expr.Filter;
			exceptionType = expr.Test;
			exception = expr.Variable;
		}

		public static void Deconstruct(
			this ConditionalExpression expr,
			out Expression condition,
			out ExpressionType operation,
			out Expression onTrue,
			out Expression onFalse)
		{
			condition = expr?.Test
						?? throw new ArgumentNullException(nameof(expr));
			operation = expr.NodeType;
			onTrue = expr.IfTrue;
			onFalse = expr.IfFalse;
		}

		public static void Deconstruct(
			this ConstantExpression expr,
			out ExpressionType operation,
			out Type constantType,
			out object val)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			val = expr.Value;
			constantType = expr.Type;
		}

		public static void Deconstruct(
			this DefaultExpression expr,
			out ExpressionType operation,
			out Type type)
		{
			type = expr?.Type
					?? throw new ArgumentNullException(nameof(expr));
			operation = expr.NodeType;
		}

		public static void Deconstruct(
			this DynamicExpression expr,
			out ExpressionType operation,
			out List<Expression> args)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			args = expr.Arguments.ToList();
		}

		public static void Deconstruct(
			this ElementInit expr,
			out List<Expression> args,
			out MethodInfo method)
		{
			args = expr?.Arguments.ToList()
					?? throw new ArgumentNullException(nameof(expr));
			method = expr.AddMethod;
		}

		public static void Deconstruct(
			this GotoExpression expr,
			out ExpressionType operation,
			out GotoExpressionKind kind,
			out LabelTarget target)
		{
			kind = expr?.Kind
					?? throw new ArgumentNullException(nameof(expr));
			operation = expr.NodeType;
			target = expr.Target;
		}

		public static void Deconstruct(
			this IndexExpression expr,
			out ExpressionType operation,
			out Expression body,
			out List<Expression> arguments)
		{
			body = expr?.Object
					?? throw new ArgumentNullException(nameof(expr));
			arguments = expr.Arguments.ToList();
			operation = expr.NodeType;
		}

		public static void Deconstruct(
			this InvocationExpression expr,
			out ExpressionType operation,
			out Expression body,
			out List<Expression> args)
		{
			body = expr?.Expression
					?? throw new ArgumentNullException(nameof(expr));
			args = expr.Arguments.ToList();
			operation = expr.NodeType;
		}

		public static void Deconstruct(
			this LabelExpression expr,
			out ExpressionType operation,
			out Expression defaultValue,
			out LabelTarget target)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			defaultValue = expr.DefaultValue;
			target = expr.Target;
		}

		public static void Deconstruct(this LabelTarget expr, out string name)
		{
			name = expr?.Name
					?? throw new ArgumentNullException(nameof(expr));
		}

		public static void Deconstruct(
			this LambdaExpression expr,
			out List<ParameterExpression> parameters,
			out Expression body)
		{
			if (expr == null)
			{
				throw new ArgumentNullException(nameof(expr));
			}

			parameters = expr.Parameters.ToList();
			body = expr.Body;
		}

		public static void Deconstruct(
			this ListInitExpression expr,
			out ExpressionType operation,
			out NewExpression newExpression,
			out List<ElementInit> initializers)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			initializers = expr.Initializers.ToList();
			newExpression = expr.NewExpression;
		}

		public static void Deconstruct(
			this LoopExpression expr,
			out ExpressionType operation,
			out Expression body,
			out LabelTarget continueLabel,
			out LabelTarget breakLabel)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			body = expr.Body;
			continueLabel = expr.ContinueLabel;
			breakLabel = expr.BreakLabel;
		}

		public static void Deconstruct(
			this MemberAssignment expr,
			out Expression body,
			out MemberInfo member,
			out MemberBindingType binding)
		{
			body = expr?.Expression
					?? throw new ArgumentNullException(nameof(expr));
			member = expr.Member;
			binding = expr.BindingType;
		}

		public static void Deconstruct(
			this MemberBinding expr,
			out MemberInfo member,
			out MemberBindingType memberType)
		{
			memberType = expr?.BindingType
						?? throw new ArgumentNullException(nameof(expr));
			member = expr.Member;
		}

		public static void Deconstruct(
			this MemberExpression expr,
			out ExpressionType operation,
			out Expression body,
			out MemberInfo member)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			body = expr.Expression;
			member = expr.Member;
		}

		public static void Deconstruct(
			this MemberInitExpression expr,
			out ExpressionType operation,
			out NewExpression newExpression,
			out List<MemberBinding> bindings)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			newExpression = expr.NewExpression;
			bindings = expr.Bindings.ToList();
		}

		public static void Deconstruct(
			this MemberListBinding expr,
			out MemberInfo member,
			out MemberBindingType bindingType,
			out List<ElementInit> initializers)
		{
			member = expr?.Member
					?? throw new ArgumentNullException(nameof(expr));
			initializers = expr.Initializers.ToList();
			bindingType = expr.BindingType;
		}

		public static void Deconstruct(
			this MemberMemberBinding expr,
			out MemberInfo member,
			out List<MemberBinding> bindings,
			out MemberBindingType bindingType)
		{
			bindings = expr?.Bindings.ToList()
						?? throw new ArgumentNullException(nameof(expr));
			member = expr.Member;
			bindingType = expr.BindingType;
		}

		public static void Deconstruct(
			this MethodCallExpression expr,
			out ExpressionType operation,
			out Expression body,
			out MethodInfo methodInfo,
			out List<Expression> args)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			body = expr.Object;
			args = expr.Arguments.ToList();
			methodInfo = expr.Method;
		}

		public static void Deconstruct(
			this NewArrayExpression expr,
			out ExpressionType operation,
			out List<Expression> expressions)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			expressions = expr.Expressions.ToList();
		}

		public static void Deconstruct(
			this NewExpression expr,
			out ExpressionType operation,
			out List<Expression> expressions,
			out List<MemberInfo> members,
			out List<Expression> args,
			out ConstructorInfo constructor)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			expressions = expr.Arguments.ToList();
			members = expr.Members.ToList();
			args = expr.Arguments.ToList();
			constructor = expr.Constructor;
		}

		public static void Deconstruct(
			this ParameterExpression expr,
			out ExpressionType operation,
			out Type type,
			out string name,
			out bool byRef)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			type = expr.Type;
			name = expr.Name;
			byRef = expr.IsByRef;
		}

		public static void Deconstruct(
			this RuntimeVariablesExpression expr,
			out ExpressionType operation,
			out List<ParameterExpression> parameters)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			parameters = expr.Variables.ToList();
		}

		public static void Deconstruct(
			this SwitchCase expr,
			out Expression body,
			out List<Expression> expressions)
		{
			body = expr?.Body
					?? throw new ArgumentNullException(nameof(expr));
			expressions = expr.TestValues.ToList();
		}

		public static void Deconstruct(
			this SwitchExpression expr,
			out Expression body,
			out Expression value,
			out List<SwitchCase> cases,
			out Expression defaultBody)
		{
			body = expr?.SwitchValue
					?? throw new ArgumentNullException(nameof(expr));
			cases = expr.Cases.ToList();
			value = expr.SwitchValue;
			defaultBody = expr.DefaultBody;
		}

		public static void Deconstruct(
			this TryExpression expr,
			out ExpressionType operation,
			out Expression body,
			out Expression fault,
			out List<CatchBlock> catches)
		{
			body = expr?.Body
					?? throw new ArgumentNullException(nameof(expr));
			fault = expr.Fault;
			catches = expr.Handlers.ToList();
			operation = expr.NodeType;
		}

		public static void Deconstruct(
			this TypeBinaryExpression expr,
			out ExpressionType operation,
			out Expression body,
			out Type type,
			out Type operand)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			body = expr.Expression;
			type = expr.Type;
			operand = expr.TypeOperand;
		}

		public static void Deconstruct(
			this UnaryExpression expr,
			out ExpressionType operation,
			out Expression body)
		{
			operation = expr?.NodeType
						?? throw new ArgumentNullException(nameof(expr));
			body = expr.Operand;
		}
	}
}
